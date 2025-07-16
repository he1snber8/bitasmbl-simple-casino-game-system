using System.Collections.Concurrent;
using Grpc.Core;

namespace CasinoGame.WalletService.Services;

public class WalletService : Wallet.WalletBase
{
    private static readonly ConcurrentDictionary<string, decimal> Balances = new();
    private static readonly List<Transaction> Transactions = new();

    static WalletService()
    {
        Balances.TryAdd("player1", 50.00m);
        Balances.TryAdd("player2", 100.00m);
    }


    public override Task<UserWallet> GetBalance(GetUserBalance request, ServerCallContext context)
    {
        if (!Balances.ContainsKey(request.UserId))
        {
            Balances.TryAdd("request.UserId", 50.00m);
        }
        
        Balances.TryGetValue(request.UserId, out var balance);

        Console.WriteLine("grpc wallet in action");

        var userWallet = new UserWallet()
        {
            UserId = request.UserId,
            Balance = (double)balance
        };

        return Task.FromResult(userWallet);
    }

    public override Task<Transaction> Credit(WalletRequest request, ServerCallContext context)
    {
        var userId = request.UserId;
        var amount = request.Amount;
        

        Console.WriteLine($"[Wallet] {userId}'s balance now: {Balances[userId]}");
        Console.WriteLine($"[Wallet] Crediting user {userId} with {amount} $$$");

        // Ensure user balance exists
        if (!Balances.ContainsKey(userId))
        {
            Balances.TryAdd(userId, 0);
        }

        // Credit balance
        Balances[userId] += (decimal)amount;

        Console.WriteLine($"[Wallet] New balance for {userId}: {Balances[userId]} $$$");

        var tx = new Transaction
        {
            UserId = userId,
            Amount = amount,
            TransactionId = $"{userId}-tx-{Guid.NewGuid()}",
            Reason = "Balance credited"
        };

        Transactions.Add(tx);


        return Task.FromResult(tx);
    }

    public override Task<WalletResponse> Debit(WalletRequest request, ServerCallContext context)
    {
        var userId = request.UserId;

        if (!string.IsNullOrEmpty(userId))
        {
            Console.WriteLine($"{userId} is found!!!");
        }

        Console.WriteLine("grpc wallet in action baby! debitting and shit");

        var amount = Convert.ToDecimal(request.Amount);
        Balances.TryAdd(userId, 0);

        Console.WriteLine($"deducting {userId} balance:{Balances[userId]} with -{amount}");

        Balances[userId] -= amount;

        var tx = new Transaction { UserId = userId, Amount = (double)amount , Reason = request.Reason};
        
        Transactions.Add(tx);

        Console.WriteLine($"Balance debited successfully, users balance now is {Balances[userId]}");

        return Task.FromResult(new WalletResponse
        {
            Balance = (double)Balances[userId],
            Message = "Balance debited successfully"
        });
    }


    public override Task<WalletResponse> Rollback(TransactionRollbackRequest request, ServerCallContext context)
    {
        var originalTx = Transactions
            .LastOrDefault(t => t.TransactionId == request.TransactionId && !t.RolledBack);

        if (originalTx == null)
        {
            return Task.FromResult(new WalletResponse
            {
                Balance = (double)Balances[request.UserId],
                Message = "Transaction not found or already rolled back"
            });
        }

        Balances[request.UserId] -= (decimal)originalTx.Amount;
        originalTx.RolledBack = true;

        return Task.FromResult(new WalletResponse
        {
            Balance = (double)Balances[request.UserId],
            Message = "Transaction rolled back"
        });
    }

    public override Task<Transaction> GetTransaction(TransactionRequest request, ServerCallContext context)
    {
        var transaction = Transactions.SingleOrDefault(t => t.TransactionId == request.TransactionId);

        if (transaction is null)
        {
            return Task.FromResult(new Transaction
            {
                Amount = 0,
                Reason = "N/A",
                RolledBack = false,
                TransactionId = "N/A",
                UserId = "N/A"
            });
        }

        return Task.FromResult(transaction);
    }

    public override Task<TransactionList> GetTransactions(UserTransactionsRequest request, ServerCallContext context)
    {
        var filteredTransactions = Transactions
            .Where(t => t.UserId == request.UserId)
            .ToList();

        var transactionList = new TransactionList();
        transactionList.Transactions.AddRange(filteredTransactions);

        return Task.FromResult(transactionList);
    }
}
