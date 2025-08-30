using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.Interfaces.Public.Upload;
using Application.Utilities;
using Application.ViewModels.Slider;
using Application.ViewModels.Slider.Request;
using Application.ViewModels.Slider.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Slider
{
    public class SliderService : ISliderService
    {
        private readonly IRepository<Domain.Entities.Slider.Slider> _sliderRepository;
        private readonly IFileUploaderService _fileUploaderService;
        private readonly IMapper _mapper;

        public SliderService(IUnitOfWorkSlider unitOfWorkSlider,
            IFileUploaderService fileUploaderService, IMapper mapper)
        {
            _sliderRepository = unitOfWorkSlider.GetRepository<Domain.Entities.Slider.Slider>();
            this._fileUploaderService = fileUploaderService;
            _mapper = mapper;
        }

        public async Task<IBusinessLogicResult<bool>> CreateNewSlider(
            RequestCreateSliderViewModel requestCreateSliderViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                //Upload
                if (requestCreateSliderViewModel.SliderFile == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                //**********    Upload Image To External Api   ***********
                var resultUpload = _fileUploaderService
                    .Upload(new List<IFormFile>() {requestCreateSliderViewModel.SliderFile})
                    .FirstOrDefault();
                //********************************************************

                if (resultUpload == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                        message: MessageId.CannotUploadFile));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                if (!string.IsNullOrEmpty(requestCreateSliderViewModel.StartDateTimeShow))
                {
                    requestCreateSliderViewModel.StartDateTimeShow =
                        requestCreateSliderViewModel.StartDateTimeShow.ConvertJalaliToMiladi().ToString();
                }
                else
                {
                    requestCreateSliderViewModel.StartDateTimeShow ??= DateTime.Now.ToString();
                }

                if (!string.IsNullOrEmpty(requestCreateSliderViewModel.EndDateTimeShow))
                {
                    requestCreateSliderViewModel.EndDateTimeShow =
                        requestCreateSliderViewModel.EndDateTimeShow.ConvertJalaliToMiladi().ToString();
                }

                var sliderEntity = _mapper.Map<Domain.Entities.Slider.Slider>(requestCreateSliderViewModel);
                sliderEntity.ImagePath = resultUpload;


                await _sliderRepository.AddAsync(sliderEntity);

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


        public async Task<IBusinessLogicResult<bool>> EditSlider(RequestEditSliderViewModel requestEditSliderViewModel,
            int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var slider = _sliderRepository.FirstOrDefaultItemAsync(x => x.Id == requestEditSliderViewModel.Id)
                    .Result;
                if (slider == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.SliderNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                }

                var oldImagePath = slider.ImagePath;

                _mapper.Map(requestEditSliderViewModel, slider);

                //Upload
                if (requestEditSliderViewModel.SliderFile != null)
                {
                    //**********    Upload Image To External Api   ***********
                    var resultUpload = _fileUploaderService
                        .Upload(new List<IFormFile>() {requestEditSliderViewModel.SliderFile})
                        .FirstOrDefault();
                    //********************************************************
                    if (resultUpload == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.CannotUploadFile));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }

                    slider.ImagePath = resultUpload;
                }
                else
                {
                    slider.ImagePath = oldImagePath;
                }

                if (!string.IsNullOrEmpty(requestEditSliderViewModel.StartDateTimeShow))
                {
                    requestEditSliderViewModel.StartDateTimeShow =
                        requestEditSliderViewModel.StartDateTimeShow.ConvertJalaliToMiladi().ToString();
                }

                if (!string.IsNullOrEmpty(requestEditSliderViewModel.EndDateTimeShow))
                {
                    requestEditSliderViewModel.EndDateTimeShow =
                        requestEditSliderViewModel.EndDateTimeShow.ConvertJalaliToMiladi().ToString();
                }

                await _sliderRepository.UpdateAsync(slider, true);

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

        public async Task<IBusinessLogicResult<ResponseGetSliderListViewModel>> GetSlider(
            RequestGetSliderListViewModel requestGetSliderListViewModel, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var slider = _sliderRepository.DeferdSelectAll();
                if (requestGetSliderListViewModel.Id > 0)
                    slider = slider.Where(s => s.Id == requestGetSliderListViewModel.Id);
                if (!string.IsNullOrEmpty(requestGetSliderListViewModel.Title))
                    slider = slider.Where(s => s.Title.Contains(requestGetSliderListViewModel.Title));
                if (!string.IsNullOrEmpty(requestGetSliderListViewModel.Description))
                    slider = slider.Where(s => s.Description.Contains(requestGetSliderListViewModel.Description));
                if (requestGetSliderListViewModel.CanShow != null)
                    slider = slider.Where(s => s.CanShow == requestGetSliderListViewModel.CanShow);

                if (!string.IsNullOrEmpty(requestGetSliderListViewModel.EndShowDateTime))
                    slider = slider.Where(s =>
                        requestGetSliderListViewModel.EndShowDateTime.ConvertJalaliToMiladi() >=
                        s.EndDateTimeShow);
                if (!string.IsNullOrEmpty(requestGetSliderListViewModel.StartShowDateTime))
                    slider = slider.Where(s =>
                        requestGetSliderListViewModel.StartShowDateTime.ConvertJalaliToMiladi() <=
                        s.StartDateTimeShow);


                if (slider == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.SliderNotFound));
                    return new BusinessLogicResult<ResponseGetSliderListViewModel>(succeeded: false, result: null,
                        messages: messages);
                }
                var sliderList = slider.ProjectTo<ResponseGetSliderViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetSliderListViewModel.Page - 1) * requestGetSliderListViewModel.PageSize)
                    .Take(requestGetSliderListViewModel.PageSize);
                

                var result = new ResponseGetSliderListViewModel
                {
                    SliderList = sliderList.ToList(),
                    Count = sliderList.Count(),
                    TotalCount = slider.Count(),
                    CurrentPage = requestGetSliderListViewModel.Page
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetSliderListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetSliderListViewModel>(succeeded: false, result: null,
                    messages: messages,
                    exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> DeleteSlider(int sliderId, int userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var slider = _sliderRepository.FirstOrDefaultItemAsync(x => x.Id == sliderId).Result;
                if (slider == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.SliderNotFound));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _sliderRepository.RemoveAsync(slider, true);

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