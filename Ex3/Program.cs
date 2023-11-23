using System;

class Program
{
    static void Main(string[] args)
    {
        CreditCard myCreditCard = new CreditCard("1234567890123456", "John Doe", "12/25", 1234, 5000, 1000);

        myCreditCard.RefillEvent += HandleRefill;
        myCreditCard.WithdrawEvent += HandleWithdraw;
        myCreditCard.StartUsingCreditEvent += HandleStartUsingCredit;
        myCreditCard.LimitReachedEvent += HandleLimitReached;
        myCreditCard.PinChangedEvent += HandlePinChanged;

        myCreditCard.Refill(2000);

        myCreditCard.Withdraw(500);

        myCreditCard.StartUsingCredit();

        myCreditCard.ChangePin(5678);
    }

    // Обробники подій
    static void HandleRefill(object sender, RefillEventArgs e)
    {
        Console.WriteLine($"Account Refilled: +{e.Amount} UAH. New Balance: {e.NewBalance} UAH");
    }

    static void HandleWithdraw(object sender, WithdrawEventArgs e)
    {
        Console.WriteLine($"Money Withdrawn: -{e.Amount} UAH. New Balance: {e.NewBalance} UAH");
    }

    static void HandleStartUsingCredit(object sender, EventArgs e)
    {
        Console.WriteLine("Started using credit.");
    }

    static void HandleLimitReached(object sender, EventArgs e)
    {
        Console.WriteLine("Credit limit reached!");
    }

    static void HandlePinChanged(object sender, EventArgs e)
    {
        Console.WriteLine("PIN has been changed.");
    }
}

class CreditCard
{
    private string _cardNumber;
    private string _ownerName;
    private string _expirationDate;
    private int _pin;
    private int _creditLimit;
    private int _balance;

    public event EventHandler<RefillEventArgs> RefillEvent;
    public event EventHandler<WithdrawEventArgs> WithdrawEvent;
    public event EventHandler StartUsingCreditEvent;
    public event EventHandler LimitReachedEvent;
    public event EventHandler PinChangedEvent;

    public CreditCard(string cardNumber, string ownerName, string expirationDate, int pin, int creditLimit, int balance)
    {
        _cardNumber = cardNumber;
        _ownerName = ownerName;
        _expirationDate = expirationDate;
        _pin = pin;
        _creditLimit = creditLimit;
        _balance = balance;
    }

    public void Refill(int amount)
    {
        _balance += amount;

        OnRefillEvent(amount);
    }

    public void Withdraw(int amount)
    {
        if (_balance >= amount)
        {
            _balance -= amount;

            OnWithdrawEvent(amount);
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
        }
    }

    public void StartUsingCredit()
    {
        if (_balance == 0)
        {
            OnStartUsingCreditEvent();
        }
        else
        {
            Console.WriteLine("Cannot start using credit. Balance is not zero.");
        }
    }

    public void ChangePin(int newPin)
    {
        _pin = newPin;

        OnPinChangedEvent();
    }

    protected virtual void OnRefillEvent(int amount)
    {
        RefillEvent?.Invoke(this, new RefillEventArgs(amount, _balance));
    }

    protected virtual void OnWithdrawEvent(int amount)
    {
        WithdrawEvent?.Invoke(this, new WithdrawEventArgs(amount, _balance));
    }

    protected virtual void OnStartUsingCreditEvent()
    {
        StartUsingCreditEvent?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnLimitReachedEvent()
    {
        LimitReachedEvent?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnPinChangedEvent()
    {
        PinChangedEvent?.Invoke(this, EventArgs.Empty);
    }
}

class RefillEventArgs : EventArgs
{
    public int Amount { get; }
    public int NewBalance { get; }

    public RefillEventArgs(int amount, int newBalance)
    {
        Amount = amount;
        NewBalance = newBalance;
    }
}

class WithdrawEventArgs : EventArgs
{
    public int Amount { get; }
    public int NewBalance { get; }

    public WithdrawEventArgs(int amount, int newBalance)
    {
        Amount = amount;
        NewBalance = newBalance;
    }
}