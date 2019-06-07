using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using URent.Models;
using URent.Helpers;
using Newtonsoft.Json.Linq;

namespace URent.Controllers
{
    public class PayPalController : Controller
    {
        private SUPContext db = new SUPContext();
        private int transactionId;
        private int itemId;
        
        /// <summary>
        /// Gets the transaction ID of a transaction
        /// </summary>
        /// <returns>Transaction ID of a transaction</returns>
        private int getTransactionId()
        {
            return transactionId;
        }

        /// <summary>
        /// Sets the transaction ID of a transaction
        /// </summary>
        /// <param name="id">ID of a transaction</param>
        private void setTransactionId(int id)
        {
            transactionId = id;
        }

        /// <summary>
        /// Gets the item ID
        /// </summary>
        /// <returns>Item ID</returns>
        private int getItemId()
        {
            return itemId;
        }

        /// <summary>
        /// Sets the item ID
        /// </summary>
        /// <param name="id">ID of an item</param>
        private void setItemId(int id)
        {
            itemId = id;
        }

        // GET: PayPal
        // NOTE: As of 2 June 2019, this view is DEPRECATED.
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult PaymentWithPaypal(int transactionId, int itemId, string Cancel = null)
        {
            //call to helper method
            setTransactionId(transactionId);
            setItemId(itemId);

            //getting the apiContext  
            APIContext apiContext = PayPalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/SUPTransactions/GetRentersTransactions?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var itID = getItemId();
            var transID = getTransactionId();
            var itemName = db.SUPItems.Where(y => y.Id == itID).Select(x => x.ItemName).FirstOrDefault().ToString();
            var price = db.SUPTransactions.Where(y => y.Id == transID).Select(x => x.TotalPrice).FirstOrDefault().ToString();
            var sku = itID.ToString();

            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = itemName,
                currency = "USD",
                price = price,
                quantity = "1",
                sku = sku
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = price
                // added price
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                // added price
                total = price, // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "URent transaction No.: " + transID.ToString(),
                invoice_number = transID.ToString(), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }

        
    }
}