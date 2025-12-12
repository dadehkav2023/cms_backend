using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.Services.WALLET.Services.Interface;
using Domain.Entities.Financial;
using Domain.Entities.Identity.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.WALLET.Services.Concrete
{
    public class WalletService : IWalletService
    {
        private readonly IRepository<Wallet> _wallets;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public WalletService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor,
            UserManager<User> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _wallets = unitOfWork.GetRepository<Wallet>();
        }

        public long ChargeWallet(FinancialTransaction financialTransaction, bool save = false)
        {
            var wallet = _wallets.DeferredWhere(x => x.UserId == financialTransaction.UserId).FirstOrDefault() ??
                         new Wallet { UserId = financialTransaction.UserId, Amount = 0 };
            wallet.Amount += financialTransaction.Amount;

            var result = _wallets.UpdateAsync(wallet, save).Result;

            return result.Amount;
        }

        public async Task<bool> SubtractPaymentFromWallet(long paymentAmount)
        {
            var userName = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username").Value;
            var userId = _userManager.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
            var wallet = await _wallets.DeferredWhere(x => x.UserId == userId).FirstOrDefaultAsync();

            if (wallet.Amount < paymentAmount)
                throw new Exception("Insufficient funds");

            wallet.Amount -= paymentAmount;

            await _wallets.UpdateAsync(wallet, false);

            return true;
        }

        public async Task<BusinessLogicResult<long>> GetWalletBalance()
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var userName = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username").Value;
                var userId = _userManager.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
                var wallet = await _wallets.DeferredWhere(x => x.UserId == userId).FirstOrDefaultAsync();

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<long>(succeeded: true, result: wallet.Amount,
                    messages: messages);
            }
            catch (Exception e)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.ErrorOccured));
                return new BusinessLogicResult<long>(succeeded: true, result: 0,
                    messages: messages, exception: e);
            }
        }
    }
}