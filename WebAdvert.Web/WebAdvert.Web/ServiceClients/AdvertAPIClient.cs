using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AdvertAPI.Models;
using Newtonsoft.Json;
using AutoMapper;
using System.Text;

namespace WebAdvert.Web.ServiceClients
{
    public class AdvertAPIClient : IAdvertAPIClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public readonly IMapper _mapper;

        public AdvertAPIClient(IConfiguration configuration, HttpClient httpClient, IMapper mapper)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _mapper = mapper;

             var createUrl = configuration.GetSection("AdvertAPI").GetValue<string>("BaseURL");
            _httpClient.BaseAddress = new Uri(createUrl);
            //_httpClient.DefaultRequestHeaders.Add("content-type", "application.json");
        }
        public async Task<AdvertResponse> Create(CreateAdvertModel model)
        {
            var advertAPIModel = _mapper.Map<AdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertAPIModel);
            var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "Advert/AddAdvert", new StringContent(jsonModel,Encoding.UTF8, "application/json"));
            var responseJson = await response.Content.ReadAsStringAsync();
            var createAdvertResponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);
            var advertResponse = _mapper.Map<AdvertResponse>(createAdvertResponse);
            return advertResponse;

        }

        public async Task<bool> Confirm(ConfirmAdvertRequest model)
        {
            var advertModel = _mapper.Map<ConfirmAdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertModel);
            var response = await _httpClient.PutAsync(_httpClient.BaseAddress + "Advert/Confirm", new StringContent(jsonModel));
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
