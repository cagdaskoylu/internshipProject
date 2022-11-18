using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query.Address;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.Address;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers.Address
{
    public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQueryRequest, ResponseBase<List<AddressDto>>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public GetAllAddressesQueryHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<List<AddressDto>>> Handle(GetAllAddressesQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<List<AddressDto>>();
            var addresses = await _addressRepository.GetAll();


            if (addresses.Count == 0)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Liste boş";

            }
            else
            {
                var addressList = _mapper.Map<IEnumerable<AddressesEntity>, IEnumerable<AddressDto>>(addresses);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Adresler getirildi";
                response.Data = addressList.ToList();
            }
            return response;
        }
    }
}
