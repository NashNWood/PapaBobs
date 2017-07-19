using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PapaBobs.DTO.Enums;

namespace PapaBobs.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void orderButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text.Trim().Length == 0)
            {
                validationLabel.Text = ("Please enter name.");
                validationLabel.Visible = true;
                return;
            }

            if (addressTextBox.Text.Trim().Length == 0)
            {
                validationLabel.Text = ("Please enter an address.");
                validationLabel.Visible = true;
                return;
            }

            if (zipTextBox.Text.Trim().Length == 0)
            {
                validationLabel.Text = ("Please enter a zip code.");
                validationLabel.Visible = true;
                return;
            }

            if (phoneTextBox.Text.Trim().Length == 0)
            {
                validationLabel.Text = ("Please enter a phone number.");
                validationLabel.Visible = true;
                return;
            }

            try
            {
                var order = buildOrder();
                Domain.OrderManager.CreateOrder(order);
                Response.Redirect("success.aspx");
            }
            catch (Exception ex)
            {
                validationLabel.Text = (ex.Message);
                validationLabel.Visible = true;
                return;
                
            }
            
        }


        private DTO.Enums.SizeType DetermineSize()
        {
            DTO.Enums.SizeType size;
            if (!Enum.TryParse(sizeDropDownList.SelectedValue, out size))
            {
                throw new Exception("Could not determine pizza size. Please choose one of the options.");
            }
            return size;
        }


        private DTO.Enums.CrustType DetermineCrust()
        {
            DTO.Enums.CrustType crust;
            if (!Enum.TryParse(crustDropDownList.SelectedValue, out crust))
            {
                throw new Exception("Could not determine pizza crust. Please choose one of the options.");
            }
            return crust;
        }


        private DTO.Enums.PaymentType DeterminePaymentType()
        {
            DTO.Enums.PaymentType paymentType;
            if (cashRadioButton.Checked)
            {
                paymentType = DTO.Enums.PaymentType.Cash;
            }
            else 
            {
                paymentType = DTO.Enums.PaymentType.Credit;
            }
            
            return paymentType;

        }

        protected void recalculateTotalCost(object sender, EventArgs e)
        {
            if (sizeDropDownList.SelectedValue == string.Empty) return;
            if (crustDropDownList.SelectedValue == string.Empty) return;
            var order = buildOrder();

            try
            {
                string total = Domain.PizzaPriceManager.CalculateCost(order).ToString("C");
                totalLabel.Text = total;
            }
            catch (Exception)
            {
                // Swallow the error
            }
           
        }

        private DTO.OrderDTO buildOrder()
        {
            var order = new DTO.OrderDTO();

            order.Size = DetermineSize();
            order.Crust = DetermineCrust();
            order.Sausage = sausageCheckBox.Checked;
            order.Pepperoni = pepperoniCheckBox.Checked;
            order.Onions = onionsCheckBox.Checked;
            order.GreenPeppers = greenPeppersCheckBox.Checked;
            order.Name = nameTextBox.Text;
            order.Address = addressTextBox.Text;
            order.Zip = zipTextBox.Text;
            order.Phone = phoneTextBox.Text;
            order.PaymentType = DeterminePaymentType();

            return order;
        }
    }
}