using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.ImageGallery.Request;
using Application.ViewModels.ImageGallery.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;

namespace Application.Services.ImageGallery
{
    public class ImageGalleryService : IImageGalleryService
    {
        private readonly IRepository<Domain.Entities.Gallery.Gallery> _galleryRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public ImageGalleryService(IUnitOfWorkGallery unitOfWorkGallery, IMapper mapper,
            IFileUploaderService fileUploaderService)
        {
            _galleryRepository = unitOfWorkGallery.GetRepository<Domain.Entities.Gallery.Gallery>();
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }


        public async Task<IBusinessLogicResult<bool>> NewGallery(RequestNewGalleryViewModel requestNewGalleryViewModel,
            int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var newGallery = _mapper.Map<Domain.Entities.Gallery.Gallery>(requestNewGalleryViewModel);

                //Upload
                var uploadResult = _fileUploaderService
                    .Upload(new List<IFormFile> { requestNewGalleryViewModel.ImageFile })
                    .FirstOrDefault();

                if (uploadResult == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                newGallery.ImagePath = uploadResult;
                await _galleryRepository.AddAsync(newGallery);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> EditGallery(
            RequestEditGalleryViewModel requestEditGalleryViewModel,
            int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var gallery = _galleryRepository.FirstOrDefaultItemAsync(g => g.Id == requestEditGalleryViewModel.Id)
                    .Result;
                if (gallery == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.GalleryItemNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldImagePath = gallery.ImagePath;
                
                _mapper.Map(requestEditGalleryViewModel, gallery);

                #region Upload

                if (requestEditGalleryViewModel.ImageFile != null)
                {
                    var uploadedFileAddress = _fileUploaderService
                        .Upload(new List< IFormFile > (){ requestEditGalleryViewModel.ImageFile })
                        .FirstOrDefault();

                    if (uploadedFileAddress == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    gallery.ImagePath = uploadedFileAddress;
                }
                else
                {
                    gallery.ImagePath = oldImagePath;
                }

                #endregion

                await _galleryRepository.UpdateAsync(gallery, true);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<ResponseGetGalleryListViewModel>> GetGallery(
            RequestGetGalleryViewModel requestGetGalleryViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var gallery = _galleryRepository.DeferdSelectAll();

                if (requestGetGalleryViewModel.Id is > 0)
                    gallery = gallery.Where(g => g.Id == requestGetGalleryViewModel.Id);
                if (!string.IsNullOrEmpty(requestGetGalleryViewModel.Title))
                    gallery = gallery.Where(g => g.Title.Contains(requestGetGalleryViewModel.Title));
                if (!string.IsNullOrEmpty(requestGetGalleryViewModel.Description))
                    gallery = gallery.Where(g => g.Description.Contains(requestGetGalleryViewModel.Description));
                if (requestGetGalleryViewModel.IsActive != null)
                    gallery = gallery.Where(g => g.IsActive == requestGetGalleryViewModel.IsActive);

                var galleryList = gallery.ProjectTo<ResponseGetGalleryViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetGalleryViewModel.Page - 1) * requestGetGalleryViewModel.PageSize)
                    .Take(requestGetGalleryViewModel.PageSize);

                var result = new ResponseGetGalleryListViewModel
                {
                    Count = galleryList.Count(),
                    CurrentPage = requestGetGalleryViewModel.Page,
                    GalleryList = galleryList.ToList(),
                    TotalCount = gallery.Count()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetGalleryListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetGalleryListViewModel>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }
            
        }

        public async Task<IBusinessLogicResult<bool>> RemoveGallery(int galleryId, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var gallery = _galleryRepository.FirstOrDefaultItemAsync(x => x.Id == galleryId).Result;
                if (gallery == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.GalleryItemNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _galleryRepository.RemoveAsync(gallery, true);
                
                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false,
                    messages: messages,
                    exception: exception);
            }

        }
    }
}