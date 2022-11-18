using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query.Address;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.Address;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers.Address
{
    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQueryRequest, ResponseBase<AddressDto>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public GetAddressByIdQueryHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<AddressDto>> Handle(GetAddressByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<AddressDto>();
            var address = await _addressRepository.GetById(request.Id);


            if (address == null)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Adres Bulunamadı";

            }
            else
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Adres getirildi";
                response.Data = _mapper.Map<AddressesEntity, AddressDto>(address);
            }
            return response;
        }

    }
}
