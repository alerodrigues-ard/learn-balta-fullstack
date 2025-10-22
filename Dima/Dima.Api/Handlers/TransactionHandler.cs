using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Amount = request.Amount,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Title = request.Title,
                Type = request.Type,
            };
        
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
        
            return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");

        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Não foi possível criar a transação");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");
                
            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            transaction.Title = request.Title;
            transaction.Type = request.Type;
            
            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();
            
             return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Não foi possível recuperar a transação");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");
            
            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Não foi possível recuperar a transação");
        }
    }

    public Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriordRequest request)
    {
        throw new NotImplementedException();
    }
}