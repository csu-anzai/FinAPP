using AutoMapper;
using BLL.Services.IServices;
using DAL.Context;
using DAL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeCategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IIncomeCategoryService _incomeCategoryService;

        public IncomeCategoryController(IMapper mapper, IIncomeCategoryService incomeCategoryService)
        {
            _mapper = mapper;
            _incomeCategoryService = incomeCategoryService;
        }
    }
}