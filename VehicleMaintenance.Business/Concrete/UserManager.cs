using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Business.CustomExtensions;
using VehicleMaintenance.Core.DataAccess;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.DataAccess.Abstract;
using VehicleMaintenance.Entity.Concrete;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSessionService _userSessionService;

        public UserManager(
            IUserDal userDal,
            ITokenService tokenService,
            IUnitOfWork unitOfWork, 
            IUserSessionService userSessionService
            )
        {
            _unitOfWork = unitOfWork;
            _userSessionService = userSessionService;
            _userDal = userDal;
            _tokenService = tokenService;
        }

        public ResponseDto AddUser(UserDto dto)
        {
            var response = new ResponseDto();

            var existingUser = _userDal.Get(p => p.Email == dto.Email);

            if (existingUser != null)
            {
                response.IsSuccess = false;
                response.Message = "Email ile kayıtlı kullanıcı bulunmaktadır.";
                return response;
            }

            var user = new User()
            {
                Email = dto.Email,
                ID = dto.ID,
                FirstName = dto.FirstName,
                CreateDate = DateTime.Now.TimeOfDay,
                Password = Encoding.UTF8.GetBytes(Hashing.HashPassword(PasswordGenerator(8))),
            };


            _userDal.Add(user);
            var savingUser = _userDal.SaveChanges();

            if (!savingUser)
            {
                response.IsSuccess = false;
                response.Message = "Kullanıcı kayıt edilirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";

            }

            return response;
        }

        public ResponseDto DeleteUser(int userId)
        {
            var response = new ResponseDto();

            var user = _userDal.Get(p => p.ID == userId);

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "Kullanıcı bulunamadı.";
                return response;
            }

            user.IsDeleted = true;
            user.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            user.ModifyDate = DateTime.Now.TimeOfDay;
            _userDal.Update(user);
            var savingUser = _userDal.SaveChanges();

            if (!savingUser)
            {
                response.IsSuccess = false;
                response.Message = "Kullanıcı silinirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";

            }


            return response;
        }

        public ResponseDto GetUser(int id)
        {
            var response = new ResponseDto();

            var user = _userDal.Get(p => p.ID == id);

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "Kullanıcı bulunamadı.";
                return response;
            }
            var userDto = new UserDto()
            {
                Email = user.Email,
                ID = user.ID,
                FirstName = user.FirstName,
            };


            response.Data = userDto;
            return response;
        }

        public ResponseDto GetUsers()
        {
            var response = new ResponseDto();

            var users = _userDal.GetList();

            if (users == null || !users.Any())
            {
                response.IsSuccess = false;
                response.Message = "Kullanıcı bulunamadı.";
                return response;
            }
            var userDtos = new List<UserDto>();

            foreach (var item in users)
            {
                var user = new UserDto()
                {
                    Email = item.Email,
                    ID = item.ID,
                    FirstName= item.FirstName,
                };
                userDtos.Add(user);
            }

            response.Data = userDtos;
            return response;
        }

        public ResponseDto Login(LoginDto login)
        {
            var response = new ResponseDto();

            var user = _userDal.Get(p => p.Email == login.Email);

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "Email adresi hatalı";
                return response;
            }

            var validatedPassword = Hashing.ValidatePassword(user.Password, login.Password);
            if (!validatedPassword)
            {
                response.IsSuccess = false;
                response.Message = "Email adresi ya da Parola hatalı.";
                return response;
            }

            var userToken = _tokenService.CreateToken(user) ;

            response.Data = new
            {
                userToken.Token,
                TokenType = "Bearer",
                Expiration = userToken.ExpireDate,
                user.FirstName
            };

            return response;
        }

        public ResponseDto Register(RegisterDto register)
        {
            var response = new ResponseDto();

            if (register.Password != register.ConfirmPassword)
            {
                response.IsSuccess = false;
                response.Message = "Parolalar uyuşmamaktadır.";
                response.Data = null;
                return response;
            }

            var existingUser = _userDal.Get(p => p.Email == register.Email);
            if (existingUser != null)
            {
                response.IsSuccess = false;
                response.Message = "Girmiş olduğunuz email adresi sistemde mevcuttur.";
                response.Data = null;
                return response;
            }

            byte[] passwordByte = Encoding.UTF8.GetBytes(Hashing.HashPassword(register.Password));

            var user = new User()
            {
                CreateDate = DateTime.Now.TimeOfDay,
                Email = register.Email,
                FirstName = register.Name,
                Password = passwordByte,
            };

            _userDal.Add(user);
            var savingUser = _userDal.SaveChanges();
            if (!savingUser)
            {
                response.IsSuccess = false;
                response.Message = "Kullanıcı kaydedilirken hata oluştu. Daha sonra tekrar deneyiniz.";
                response.Data = null;
                return response;
            }

            return response;
        }

        public ResponseDto UpdateUser(UserDto dto)
        {
            var response = new ResponseDto();

            var existingUser = _userDal.Get(p => p.Email == dto.Email);

            if (existingUser == null)
            {
                response.IsSuccess = false;
                response.Message = "Kullanıcı bulunamadı.";
                return response;
            }

            if (existingUser.Email != dto.Email)
            {
                var hasEmailedUser = _userDal.Get(p => p.Email == dto.Email);
                if (hasEmailedUser != null)
                {
                    response.IsSuccess = false;
                    response.Message = "Güncellemek istediğiniz email adresine bağlı kullanıcı bulunmaktadır.";
                    return response;
                }
            }


            if (!String.IsNullOrEmpty(dto.Password))
            {
                existingUser.Password = Encoding.UTF8.GetBytes(Hashing.HashPassword(dto.Password));
            }
            existingUser.FirstName = dto.FirstName;
            existingUser.Email = dto.Email;
            existingUser.ModifyDate = DateTime.Now.TimeOfDay;

            _userDal.Update(existingUser);
            var savingUser = _userDal.SaveChanges();

            if (!savingUser)
            {
                response.IsSuccess = false;
                response.Message = "Kullanıcı kayıt edilirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";

            }

            return response;

        }

        private string PasswordGenerator(int size)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            char[] chars = new char[size];
            for (int i = 0; i < size - 1; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            var nonAlphaNumeric = "!@#$%^&*?_-";
            chars[size - 1] = nonAlphaNumeric[random.Next(0, nonAlphaNumeric.Length)];

            return new string(chars);
        }

    }
}
