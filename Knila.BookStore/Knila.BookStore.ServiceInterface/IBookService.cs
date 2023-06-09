﻿using Knila.BookStore.Domain;

namespace Knila.BookStore.ServiceInterface
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBookDetailsSort1Async();

        Task<List<Book>> GetAllBookDetailsSort2Async();

        Task LoadBookDataAsync(int count);

        Task<double> GetTotalBookPriceAsync();

        Task<int> GetTotalNumberOfBooksAsync();

        Task<List<Book>> GetLast5BookDetailsAsync();
    }
}