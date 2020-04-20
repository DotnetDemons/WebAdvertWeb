using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertAPI.Models;

namespace WebAdvert.Web.ServiceClients
{
    public interface IAdvertAPIClient
    {
        Task<AdvertResponse> Create(CreateAdvertModel model);
        Task<bool> Confirm(ConfirmAdvertRequest model);
        Task<List<CreateAdvertModel>> Get(string id);
    }
}
