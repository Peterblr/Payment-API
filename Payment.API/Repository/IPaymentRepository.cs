using Payment.API.Models;

namespace Payment.API.Repository
{
    public interface IPaymentRepository
    {
        ////Get Item
        //Task<PaymentDetail> GetPaymentAsync(int id);
        ////Get List Items
        //Task<IEnumerable<PaymentDetail>> GetAllPaymentsAsync();
        ////Create Item
        //Task CreatePaymentAsync(PaymentDetail payment);
        ////Update Item
        //Task UpdatePaymentAsync(PaymentDetail payment);
        ////Delete Item
        //Task DeletePaymentAsync(int id);


        /////////////////**************/////////////////
        PaymentDetail GetPayment(int id);
        IEnumerable<PaymentDetail> GetAllPayments();
        void CreatePayment(PaymentDetail payment);
        void UpdatePayment(PaymentDetail payment);
        void DeletePayment(int id);

    }
}
