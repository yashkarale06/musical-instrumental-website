using Razorpay.Api;
using System;
using System.Collections.Generic;

namespace MusicalInstrumentShop.Services
{
    public class RazorpayService
    {
        private readonly string _keyId;
        private readonly string _keySecret;
        private readonly RazorpayClient _client;

        public RazorpayService(string keyId, string keySecret)
        {
            _keyId = keyId;
            _keySecret = keySecret;
            _client = new RazorpayClient(_keyId, _keySecret);
        }

        public string CreateOrder(decimal amount, string currency, string receipt)
        {
            try
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", amount * 100); // Razorpay expects amount in paise
                options.Add("currency", currency);
                options.Add("receipt", receipt);
                options.Add("payment_capture", 1); // Auto-capture payment

                Order order = _client.Order.Create(options);
                return order["id"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Razorpay order: " + ex.Message);
            }
        }

        public bool VerifyPaymentSignature(string orderId, string paymentId, string signature)
        {
            try
            {
                Dictionary<string, string> attributes = new Dictionary<string, string>();
                attributes.Add("razorpay_order_id", orderId);
                attributes.Add("razorpay_payment_id", paymentId);
                attributes.Add("razorpay_signature", signature);

                Utils.verifyPaymentSignature(attributes);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}