using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Transaction
{
    public class CreateModel
    {
        public int BudgetId { get; set; }
        
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }

        public IEnumerable<SelectListItem> AccountList { get; set; }
        [Display(Name = "Hesap")]
        public int AccountId { get; set; }

        public IEnumerable<SelectListItem> TargetAccountList { get; set; }
        [Display(Name = "Hedef Hesap")]
        public int? TargetAccountId { get; set; }
        
        public bool IsSplitTransaction { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        [Display(Name = "Kategori")]
        public int? CategoryId { get; set; }

        [Display(Name = "Tutar")]
        public decimal Amount { get; set; }

        [Display(Name ="Gelir Mi?")]
        public bool IsIncome { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        public int ActionAccountId { get; set; }
    }
}
