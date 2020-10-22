using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.WebUI.Models.Transaction
{
    public class DetailModel
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }

        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }

        [Display(Name = "Hesap")]
        public string AccountName { get; set; }

        //public int? TargetAccountId { get; set; }
        //public bool IsSplitTransaction { get; set; }

        [Display(Name = "Kategori")]
        public string CategoryName { get; set; }

        [Display(Name = "Tutar")]
        public decimal Amount { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }
    }
}
