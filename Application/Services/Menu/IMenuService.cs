using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.Menu.Request;
using AutoMapper;
using Domain.Entities.MenuComposite;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Menu
{
    public interface IMenuService
    {
        Task<IBusinessLogicResult<bool>> AddMenu(MenuViewModelRequest request);
        Task<IBusinessLogicResult<bool>> AddMenuItem(MenuItemViewModelRequest request);
        Task<IBusinessLogicResult<List<MenuComponentResult>>> GetMenu(int depth);
        Task<IBusinessLogicResult<MenuItem>> GetMenuById(int Id);
        Task<IBusinessLogicResult<Domain.Entities.MenuComposite.Menu>> GetRootMenuById(int Id);
        Task<IBusinessLogicResult<bool>> EditMenu(MenuViewModelRequest request);
        Task<IBusinessLogicResult<bool>> EditMenuItem(MenuItemViewModelRequest request);
        Task<IBusinessLogicResult<bool>> RemoveMenu(int Id);
    }

    public class MenuService : IMenuService
    {
        private readonly IRepository<MenuComponent> repository;
        private readonly IRepository<MenuItem> repositoryMenuItem;
        private readonly IRepository<Domain.Entities.MenuComposite.Menu> repositoryMenu;
        private readonly IMapper mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public MenuService(IUnitOfWorkMenu unitOfWorkMenu, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            this.repository = unitOfWorkMenu.GetRepository<MenuComponent>();
            this.repositoryMenuItem = unitOfWorkMenu.GetRepository<MenuItem>();
            this.repositoryMenu = unitOfWorkMenu.GetRepository<Domain.Entities.MenuComposite.Menu>();
            this.mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }
        public async Task<IBusinessLogicResult<bool>> AddMenu(MenuViewModelRequest request)
        {
            var messages = new List<BusinessLogicMessage>();
            if (request.Id == 0)
            {
                request.IsRoot = true;

                var _menu = mapper.Map<Domain.Entities.MenuComposite.Menu>(request);

                #region Upload
                if (request.IconPath != null)
                {
                    var uploadedAddress = _fileUploaderService
                    .Upload(new List<IFormFile>() { request.IconPath })
                    .FirstOrDefault();

                    if (uploadedAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }
                    _menu.IconPath = uploadedAddress;
                }
                #endregion

                await repository.AddAsync(_menu);
            }
            else
            {
                var _Menu = await repository.FirstOrDefaultItemAsync(x => x.Id == request.Id);
                if (_Menu == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                    return new BusinessLogicResult<bool>(succeeded: false, result: true, messages: messages);
                }
                request.Id = 0;
                request.IsRoot = false;
                
                var _menu = mapper.Map<Domain.Entities.MenuComposite.Menu>(request);

                #region Upload
                if (request.IconPath != null)
                {
                    var uploadedAddress = _fileUploaderService
                    .Upload(new List<IFormFile>() { request.IconPath })
                    .FirstOrDefault();

                    if (uploadedAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }
                    _menu.IconPath = uploadedAddress;
                }
                #endregion
                
               

                _Menu.Add(_menu);
                await repository.UpdateAsync(_Menu, true);

            }

            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
        }

        public async Task<IBusinessLogicResult<bool>> AddMenuItem(MenuItemViewModelRequest request)
        {
            var messages = new List<BusinessLogicMessage>();

            var _Menu = await repository.FirstOrDefaultItemAsync(x => x.Id == request.Id);
            if(_Menu==null)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: true, messages: messages);
            }

            request.Id = 0;
            request.IsRoot = false;


            var _menu = mapper.Map<Domain.Entities.MenuComposite.MenuItem>(request);


            #region Upload Icon
            if (request.IconPath != null)
            {
                var uploadedAddress = _fileUploaderService
                    .Upload(new List<IFormFile>() { request.IconPath })
                    .FirstOrDefault();

                if (uploadedAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }
                _menu.IconPath = uploadedAddress;
            }
            else _menu.IconPath = null;
            #endregion

            #region Upload File
            if (request.File != null)
            {
                var uploadedAddressFile = _fileUploaderService
                    .Upload(new List<IFormFile>() { request.File })
                    .FirstOrDefault();

                if (uploadedAddressFile == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                _menu.FilePath = uploadedAddressFile;
            }
            else _menu.FilePath = null;
            #endregion

            _Menu.Add(_menu);// new MenuItem(request.Title, "", request.IsActive, request.CKEditorText, request.Link, request.FilePath, request.ModularPage));
            await repository.UpdateAsync(_Menu, true);

            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
        }

        public async Task<IBusinessLogicResult<bool>> EditMenu(MenuViewModelRequest request)
        {
            var messages = new List<BusinessLogicMessage>();
            if (request.Id > 0)
            {
                var _MenuFind = await repository.FirstOrDefaultItemAsync(x => x.Id == request.Id);
                if (_MenuFind == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                    return new BusinessLogicResult<bool>(succeeded: false, result: true, messages: messages);
                }
                string OldPath = _MenuFind.IconPath;
                mapper.Map(request, _MenuFind);
                _MenuFind.IconPath = OldPath;

                if (request.IconPath != null)
                {
                    #region Upload
                    var uploadedAddress = _fileUploaderService
                        .Upload(new List<IFormFile>() { request.IconPath })
                        .FirstOrDefault();

                    if (uploadedAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }
                    _MenuFind.IconPath = uploadedAddress;
                    #endregion
                }



                await repository.UpdateAsync(_MenuFind, true);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            else
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                       message: MessageId.ValidationCodeIsWrong));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
            }
        }

        public async Task<IBusinessLogicResult<bool>> EditMenuItem(MenuItemViewModelRequest request)
        {
            var messages = new List<BusinessLogicMessage>();

            var _Menu_Find = await repositoryMenuItem.FirstOrDefaultItemAsync(x => x.Id == request.Id);
            if (_Menu_Find == null)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: true, messages: messages);
            }

            request.IsRoot = false;
            var OldPath = _Menu_Find.IconPath;
            var OldFile = _Menu_Find.IconPath;
            mapper.Map(request, _Menu_Find);
            _Menu_Find.IconPath = OldPath;
            _Menu_Find.FilePath = null;

            if (request.IconPath != null)
            {
                #region Upload
                var uploadedAddress = _fileUploaderService
                    .Upload(new List<IFormFile>() { request.IconPath })
                    .FirstOrDefault();
                if (uploadedAddress == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }
                _Menu_Find.IconPath = uploadedAddress;
                #endregion
            }

            #region Upload File
            if (request.File != null)
            {
                var uploadedAddressFile = _fileUploaderService
                    .Upload(new List<IFormFile>() { request.File })
                    .FirstOrDefault();

                if (uploadedAddressFile == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                _Menu_Find.FilePath = uploadedAddressFile;
            }
            #endregion

            await repository.UpdateAsync(_Menu_Find, true);

            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
        }

        public async Task<IBusinessLogicResult<List<MenuComponentResult>>> GetMenu(int depth)
        {
            var messages = new List<BusinessLogicMessage>();

            var _menu = repository.DeferdSelectAll().ToList();
            _menu = _menu.Where(p => p.GetType() == typeof(Domain.Entities.MenuComposite.Menu)).ToList();
            _menu = _menu.Where(p => p.IsRoot).ToList();

            //var result = mapper.Map<MenuComponentResponse>(_menu);
           

            var ResultMapping = CreateMenu(_menu);

           
            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<List<MenuComponentResult>> (succeeded: true, result: ResultMapping, messages: messages);
        }

        public async Task<IBusinessLogicResult<MenuItem>> GetMenuById(int Id)
        {
            var messages = new List<BusinessLogicMessage>();

            var _menu = repositoryMenuItem.Find(Id);

            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<MenuItem>(succeeded: true, result: _menu, messages: messages);
        }

        public async Task<IBusinessLogicResult<Domain.Entities.MenuComposite.Menu>> GetRootMenuById(int Id)
        {
            var messages = new List<BusinessLogicMessage>();

            var _menu = repositoryMenu.Find(Id);

            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<Domain.Entities.MenuComposite.Menu>(succeeded: true, result: _menu, messages: messages);
        }

        public async Task<IBusinessLogicResult<bool>> RemoveMenu(int Id)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var _Menu_Find = repository.Find(Id);
                if (_Menu_Find == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                    return new BusinessLogicResult<bool>(succeeded: false, result: true, messages: messages);
                }

                var removeMenu = repository.RemoveAsync(_Menu_Find, true).Result;
                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch(Exception exp)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                       message: MessageId.InternalError));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
            }
        }

        private List<MenuComponentResult> CreateMenu(List<MenuComponent> menu)
        {
            List<MenuComponentResult> menusResult = new List<MenuComponentResult>();
            foreach (var item in menu)
            {
                if (item.GetType() == typeof(Domain.Entities.MenuComposite.Menu))
                {
                    var itemMenu = new MenuComponentResult();
                    var _item = item as Domain.Entities.MenuComposite.Menu;

                    itemMenu.Id = _item.Id;
                    itemMenu.Title = _item.Title;
                    itemMenu.IconPath = _item.IconPath;
                    itemMenu.IsActive = _item.IsActive;
                    if (_item.MenuItem.Count > 0) itemMenu.menuComponentResponses = CreateMenu(_item.MenuItem.ToList());
                    menusResult.Add(itemMenu);
                }
                if (item.GetType() == typeof(Domain.Entities.MenuComposite.MenuItem))
                {
                    var itemMenu = new MenuComponentResult();
                    var _item = item as Domain.Entities.MenuComposite.MenuItem;

                    itemMenu.Id = _item.Id;
                    itemMenu.Title = _item.Title;
                    itemMenu.IconPath = _item.IconPath;
                    itemMenu.IsActive = _item.IsActive;
                    itemMenu.menuItem = new MenuItemResult()
                    {
                        CKEditorText = _item.CKEditorText,
                        Link = _item.Link,
                        FilePath = _item.FilePath,
                        ModularPage = _item.ModularPage,
                    };

                    menusResult.Add(itemMenu);
                }
            }

            return menusResult;
        }

        //private List<MenuComponentResponse> CreateMenu(List<MenuComponent> menu)
        //{
        //    List<MenuComponentResponse> menusResult = new List<MenuComponentResponse>();
        //    foreach (var item in menu)
        //    {
        //        if (item.GetType() == typeof(Domain.Entities.MenuComposite.Menu))
        //        {
        //            var itemMenu = new MenuViewModelResponse();
        //            var _item = item as Domain.Entities.MenuComposite.Menu;

        //            itemMenu.Id = _item.Id;
        //            itemMenu.Title = _item.Title;
        //            itemMenu.IconPath = _item.IconPath;
        //            itemMenu.IsActive = _item.IsActive;
        //            if (_item.MenuItem.Count > 0) itemMenu.menuComponentResponses = CreateMenu(_item.MenuItem.ToList());
        //            menusResult.Add(itemMenu);
        //        }
        //        if (item.GetType() == typeof(Domain.Entities.MenuComposite.MenuItem))
        //        {
        //            var itemMenuItem = new MenuItemViewModelResponse();
        //            var _item = item as Domain.Entities.MenuComposite.MenuItem;

        //            itemMenuItem.Id = _item.Id;
        //            itemMenuItem.Title = _item.Title;
        //            itemMenuItem.IconPath = _item.IconPath;
        //            itemMenuItem.IsActive = _item.IsActive;
        //            itemMenuItem.CKEditorText = _item.CKEditorText;
        //            itemMenuItem.Link = _item.Link;
        //            itemMenuItem.FilePath = _item.FilePath;
        //            itemMenuItem.ModularPage = _item.ModularPage;
        //            menusResult.Add(itemMenuItem);
        //        }
        //    }

        //    return menusResult;
        //}


    }
}
