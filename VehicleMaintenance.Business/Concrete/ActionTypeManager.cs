﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Core.DataAccess;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.DataAccess.Abstract;
using VehicleMaintenance.Entity.Concrete;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Concrete
{
    public class ActionTypeManager : IActionTypeService
    {
        private readonly IActionTypeDal _actionTypeDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSessionService _userSessionService;
        public ActionTypeManager(IActionTypeDal actionTypeDal, IUnitOfWork unitOfWork, IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
            _actionTypeDal = actionTypeDal;
            _unitOfWork = unitOfWork;
        }
        public ResponseDto AddActionType(ActionTypeDto dto)
        {
            var response = new ResponseDto();
            var existingActionType = _actionTypeDal.Get(x => x.Name == dto.Name);

            if (existingActionType != null)
            {
                response.IsSuccess = false;
                response.Message = "Aynı isme ait bir aksiyon mevcut";
                return response;
            }

            var actionType = new ActionType()
            {
                CreateDate = DateTime.Now.TimeOfDay,
                CreatedByUser = _unitOfWork.GetRepository<User>().Get(p=> p.ID == _userSessionService.GetUserId()),
                IsDeleted = false,
                Name = dto.Name,
            };

            _actionTypeDal.Add(actionType);
            var savingActionType = _actionTypeDal.SaveChanges();

            if (!savingActionType)
            {
                response.IsSuccess = false;
                response.Message = "Kayıt esnasında bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = actionType;

            return response;
        }

        public ResponseDto UpdateActionType(ActionTypeDto actionTypeDto)
        {
            var response = new ResponseDto();

            var existingActionType = _actionTypeDal.Get(p => p.ID == actionTypeDto.ID);

            if (existingActionType == null)
            {
                response.IsSuccess = false;
                response.Message = "Aksiyon tipi bulunamadı.";
                return response;
            }

            existingActionType.ModifyDate = DateTime.Now.TimeOfDay;
            //existingActionType.ModifiedBy = User
            existingActionType.Name = actionTypeDto.Name;

            _actionTypeDal.Update(existingActionType);
            var savingActionType = _actionTypeDal.SaveChanges();

            if (!savingActionType)
            {
                response.IsSuccess = false;
                response.Message = "Aksiyon tipi güncellenirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            return response;

        }

        public ResponseDto DeleteActionType(int id)
        {
            var response = new ResponseDto();

            var actionType = _actionTypeDal.Get(p => p.ID == id);

            if (actionType == null)
            {
                response.IsSuccess = false;
                response.Message = "Aksiyon tipi bulunamadı.";
                return response;
            }

            //actionType.ModifiedBy = Kullanıcı
            actionType.ModifyDate = DateTime.Now.TimeOfDay;
            actionType.IsDeleted = true;
            _actionTypeDal.Update(actionType);
            var savingActionType = _actionTypeDal.SaveChanges();

            if (!savingActionType)
            {
                response.IsSuccess = false;
                response.Message = "Aksiyon tipi silinirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";

            }

            return response;
        }

        public ResponseDto GetActionTypeById(int id)
        {
            var response = new ResponseDto();
            var actionType = _actionTypeDal.Get(x => x.ID == id && x.IsDeleted == false);
            if (actionType == null)
            {
                response.IsSuccess = false;
                response.Message = "Aksiyon Tipi bulunamadı.";
                return response;
            }

            var actionTypeDto = new ActionTypeDto()
            {
                ID = actionType.ID,
                Name = actionType.Name
            };

            response.Data = actionTypeDto;
            return response;
        }

        public ResponseDto GetAllActionType()
        {
            var response = new ResponseDto();

            var actionTypes = _actionTypeDal.GetList();

            if (actionTypes == null || !actionTypes.Any())
            {
                response.IsSuccess = false;
                response.Message = "Aksiyon tipi bulunamadı.";
                return response;
            }
            var actionTypeDtos = new List<ActionTypeDto>();

            foreach (var actionType in actionTypeDtos)
            {
                var actionTypeDto = new ActionTypeDto()
                {
                    ID = actionType.ID,
                    Name = actionType.Name,
                };

                actionTypeDtos.Add(actionTypeDto);
            }

            response.Data = actionTypeDtos;
            return response;
        }
    }
}