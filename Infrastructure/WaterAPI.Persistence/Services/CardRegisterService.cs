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
        
        ICardRegisterWriteRepository _repository;

        public CardRegisterService(ICardRegisterWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateCardRegisterResponseDTO> CreateAsync(CreateCardRegisterRequestDTO model)
        {
        CreateCardRegisterResponseDTO response = new CreateCardRegisterResponseDTO();
            bool result = await _repository.AddAsync(new()
            {
                Name = model.Name,
                Number = model.Number,
            });

            if (result) 
            {
                response.Massage = "Kart Eklendi.";
            }
            else
                throw new Exception("Kart Eklenemedi.");
        
            return response;
        }
    }
}
