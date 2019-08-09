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
    public class ExpenseCategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoryController(IMapper mapper, IExpenseCategoryService expenseCategoryService)
        {
            _mapper = mapper;
            _expenseCategoryService = expenseCategoryService;
        }

       
    }
}