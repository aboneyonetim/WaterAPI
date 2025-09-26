using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Services;
using WaterAPI.Application.DTOs.CardRegister;
using WaterAPI.Application.Repositories;

namespace WaterAPI.Persistence.Services
{
    public class CardRegisterService : ICardRegisterService
    {
        
        ICardRegisterReadRepository _cardRegisterReadRepository;
        ICardRegisterWriteRepository _cardRegisterWriteRepository;

        public CardRegisterService(ICardRegisterReadRepository cardRegisterReadRepository,
            ICardRegisterWriteRepository cardRegisterWriteRepository)
        {
            _cardRegisterReadRepository = cardRegisterReadRepository;
            _cardRegisterWriteRepository = cardRegisterWriteRepository;
        }



        public async Task<CreateCardRegisterResponseDTO> CreateAsync(CreateCardRegisterRequestDTO model)
        {

            bool cardExist = _cardRegisterReadRepository.GetWhere(c =>c.AppUserId==model.AppUserId && c.Number == model.Number).Any();

            if (cardExist)
            {// Eğer kart zaten mevcutsa, başarılı olmadığını belirten bir yanıt döndür.
                return new CreateCardRegisterResponseDTO
                {
                    Succeeded = false,
                    Message = "Bu kart numarası bu kullanıcı için zaten kayıtlı."
                };
            }




            CreateCardRegisterResponseDTO response = new CreateCardRegisterResponseDTO();


           
            bool result = await _cardRegisterWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Number = model.Number,
                AppUserId = model.AppUserId,

            });
            await _cardRegisterWriteRepository.SaveAsync();
            response.Succeeded = result;

            // 4. 'Succeeded' alanına göre mesajı düzenle.
            if (response.Succeeded)
            {
                response.Message = "Kart başarıyla eklendi.";
            }
            else
            {
                response.Message = "Kart eklenirken bir hata oluştu.";
            }
            return response;
        }
    }
}
