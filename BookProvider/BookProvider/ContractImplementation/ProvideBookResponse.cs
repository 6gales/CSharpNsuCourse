using System;
using System.Collections.Generic;
using BookShop.MessageContract;

namespace BookProvider.ContractImplementation
{
    internal sealed class ProvideBooksResponse : IProvideBooksResponse
    {
        public ProvideBooksResponse(IReadOnlyCollection<IBookResponse> providedBooks, decimal totalCost)
        {
            ProvidedBooks = providedBooks;
            TotalCost = totalCost;
        }

        public IReadOnlyCollection<IBookResponse> ProvidedBooks { get; }
        public decimal TotalCost { get; }
    }

    public class JsonBook : IBookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Price { get; set; }
        public bool IsNew { get; set; }
        public DateTime DateOfDelivery { get; set; }
    }
}