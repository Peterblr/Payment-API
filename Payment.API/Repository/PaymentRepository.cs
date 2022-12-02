using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Payment.API.Data;
using Payment.API.Models;

namespace Payment.API.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext context;
        private readonly IConfiguration configuration;
        private readonly string dbcon;

        public PaymentRepository(PaymentDbContext context,
            IConfiguration configuration
            )
        {
            this.context = context;
            this.configuration = configuration;
            dbcon = this.configuration["ConnectionStrings:DevConnection"];
        }

        //public async Task CreatePaymentAsync(PaymentDetail payment)
        //{
        //    await context.PaymentDetails.AddAsync(payment);

        //    await context.SaveChangesAsync();
        //}

        //public async Task DeletePaymentAsync(int id)
        //{
        //    var payment = await context.PaymentDetails.FindAsync(id);

        //    if (payment != null)
        //    {
        //        context.PaymentDetails.Remove(payment);
        //    }

        //    await context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<PaymentDetail>> GetAllPaymentsAsync()
        //{
        //    return await context.PaymentDetails.ToListAsync();
        //}

        //public async Task<PaymentDetail> GetPaymentAsync(int id)
        //{
        //    var payment = await context.PaymentDetails.FindAsync(id);

        //    return payment;
        //}

        //public async Task UpdatePaymentAsync(PaymentDetail payment)
        //{
        //    context.Entry(payment).State = EntityState.Modified;

        //    await context.SaveChangesAsync();
        //}


        /////////////////////////*********//////////////////////////

        //public PaymentDetail GetPayment(int id)
        //{
        //    var payment = new PaymentDetail();

        //    using (SqlConnection connection = new(dbcon))
        //    {
        //        SqlCommand cmd = new()
        //        {
        //            Connection = connection,
        //        };

        //        string query = "SELECT * FROM [PaymentDetails] WHERE [PaymentDetailId]=" + id + ";";
        //        cmd.CommandText = query;

        //        connection.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            payment.PaymentDetailId = (int)reader["PaymentDetailId"];
        //            payment.CardOwnerName = (string)reader["CardOwnerName"];
        //            payment.CardNumber = (string)reader["CardNumber"];
        //            payment.ExpirationDate = (string)reader["ExpirationDate"];
        //            payment.SecurityCode = (string)reader["SecurityCode"];
        //        }
        //    }

        //    return payment;
        //}

        public PaymentDetail GetPayment(int id)
        {
            var payment = new PaymentDetail();

            using (SqlConnection connection = new(dbcon))
            {              
                connection.Open();
                using (SqlCommand cmd = new("spPaymentDetails_GetPaymentById", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        payment.PaymentDetailId = (int)reader["PaymentDetailId"];
                        payment.CardOwnerName = (string)reader["CardOwnerName"];
                        payment.CardNumber = (string)reader["CardNumber"];
                        payment.ExpirationDate = (string)reader["ExpirationDate"];
                        payment.SecurityCode = (string)reader["SecurityCode"];
                    }
                }
            }

            return payment;
        }


        public IEnumerable<PaymentDetail> GetAllPayments()
        {
            var payments = new List<PaymentDetail>();

            using (SqlConnection connection = new(dbcon))
            {
                SqlCommand cmd = new()
                {
                    Connection = connection,
                };

                string query = "SELECT * FROM [PaymentDetails];";
                cmd.CommandText = query;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var payment = new PaymentDetail()
                    {
                        PaymentDetailId = (int)reader["PaymentDetailId"],
                        CardOwnerName = (string)reader["CardOwnerName"],
                        CardNumber = (string)reader["CardNumber"],
                        ExpirationDate = (string)reader["ExpirationDate"],
                        SecurityCode = (string)reader["SecurityCode"],
                    };  
                    payments.Add(payment);
                }
            }

            return payments;

        }

        public void CreatePayment(PaymentDetail payment)
        {
            using (SqlConnection connection = new(dbcon))
            {
                SqlCommand cmd = new()
                {
                    Connection = connection,
                };

                string query = "INSERT INTO [PaymentDetails] ([CardOwnerName], [CardNumber], [ExpirationDate], [SecurityCode]) " +
                    "VALUES (@coname, @cnum, @edate, @scode);";
                
                cmd.CommandText = query;
                
                cmd.Parameters.Add("@coname", System.Data.SqlDbType.NVarChar).Value = payment.CardOwnerName;
                cmd.Parameters.Add("@cnum", System.Data.SqlDbType.NVarChar).Value = payment.CardNumber;
                cmd.Parameters.Add("@edate", System.Data.SqlDbType.NVarChar).Value = payment.ExpirationDate;
                cmd.Parameters.Add("@scode", System.Data.SqlDbType.NVarChar).Value = payment.SecurityCode;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePayment(PaymentDetail payment)
        {
            using (SqlConnection connection = new(dbcon))
            {
                SqlCommand cmd = new()
                {
                    Connection = connection,
                };

                string query = "UPDATE [PaymentDetails] SET [CardOwnerName] = @coname, [CardNumber] = @cnum, [ExpirationDate] = @edate, [SecurityCode] = @scode " +
                    "WHERE [PaymentDetailId] = " + payment.PaymentDetailId + ";";

                cmd.CommandText = query;

                cmd.Parameters.Add("@coname", System.Data.SqlDbType.NVarChar).Value = payment.CardOwnerName;
                cmd.Parameters.Add("@cnum", System.Data.SqlDbType.NVarChar).Value = payment.CardNumber;
                cmd.Parameters.Add("@edate", System.Data.SqlDbType.NVarChar).Value = payment.ExpirationDate;
                cmd.Parameters.Add("@scode", System.Data.SqlDbType.NVarChar).Value = payment.SecurityCode;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePayment(int id)
        {
            using (SqlConnection connection = new(dbcon))
            {
                SqlCommand cmd = new()
                {
                    Connection = connection,
                };

                string query = "DELETE FROM [PaymentDetails] WHERE [PaymentDetailId]=" + id + ";";
                cmd.CommandText = query;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
