using System;

// Step 1: Missing a Common Payment Interface
public interface IPayment
{
    public void Makepayment(double amount);
}
public class PayPalPayment:IPayment
{
    public void Makepayment(double amount)
    {
        Console.WriteLine($"Payment of ${amount} made through PayPal.");
    }
}

// Step 2: Adaptee does not adhere to existing conventions
public class StripePayment
{
    public void ProcessStripePayment(double amount, string currency)
    {
        Console.WriteLine($"Payment of {currency}{amount} processed through Stripe.");
    }
}

// Step 3: Adapter tightly couples the client to PayPalPayment and StripePayment
public class PaymentAdapter : IPayment
{
    private StripePayment _stripePayment;
    public string _Currency { get; set; }

    public PaymentAdapter(StripePayment stripePayment,string Currency)
    {
        _stripePayment = stripePayment;
        _Currency = Currency;
    }

    // Mixing PayPal logic with Stripe logic
    public void Makepayment(double amount)
    {
        _stripePayment.ProcessStripePayment(amount, _Currency);
    }
}

// Step 4: Tightly Coupled Client Code
class Program
{
    static void Main(string[] args)
    {
        // Client tightly coupled to PaymentAdapter

        IPayment paypal = new PayPalPayment();
        paypal.Makepayment(100);
        StripePayment stripe = new StripePayment();
        PaymentAdapter adapter = new PaymentAdapter(stripe,"Card");
        adapter.Makepayment(100);
        Console.ReadLine();
    }
}
