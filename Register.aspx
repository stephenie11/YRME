<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
<div class="container">
    <div class="row">
		<div class="col-md-4 col-md-offset-4">
    		<div class="panel panel-default">
			  	<div class="panel-heading">
			    	<h3 class="panel-title">Please register</h3>
			 	</div>
			  	<div class="panel-body">
			    	<form accept-charset="UTF-8" role="form">
                    <fieldset>
                        <!--First name-->
			    	  	<div class="form-group">
                            <asp:TextBox ID="user_reg_firstname" runat="server" class="form-control" placeholder="First Name"/>
                            <asp:RequiredFieldValidator ID="reg_firstname_validation" runat="server" ControlToValidate="user_reg_firstname" ErrorMessage="Please let us know your first name!" ForeColor="Red"></asp:RequiredFieldValidator>
			    		</div>
                        <!--Last name-->
			    	  	<div class="form-group">
                            <asp:TextBox ID="user_reg_lastname" runat="server" class="form-control" placeholder="Last Name"/>
                            <asp:RequiredFieldValidator ID="reg_lastname_validation" runat="server" ControlToValidate="user_reg_lastname" ErrorMessage="Please let us know your last name!" ForeColor="Red"></asp:RequiredFieldValidator>
			    		</div>
                        <!--Email-->
			    	  	<div class="form-group">
                            <asp:TextBox ID="user_reg_email" runat="server" class="form-control" placeholder="Email"/>
                            <asp:RequiredFieldValidator ID="reg_e_validation" runat="server" ControlToValidate="user_reg_email" ErrorMessage="Please fill with your email address!" ForeColor="Red"></asp:RequiredFieldValidator>
			    		</div>
                        <!--Password-->
			    		<div class="form-group">
                            <asp:TextBox class="form-control" placeholder="Password" ID="user_reg_password" TextMode="Password" runat="server" />
                            <asp:RequiredFieldValidator ID="reg_p_validation" runat="server" ControlToValidate="user_reg_password" ErrorMessage="Please fill with your password!" ForeColor="Red"></asp:RequiredFieldValidator>
			    		</div>
			    		<!--Register button-->
                        <asp:Button class="btn btn-lg btn-success btn-block" Text="Register" runat="server" ID="Register_button" OnClick="Register_button_Click" /><br />
                        <!--Errors literal register-->
                        <p><asp:Label ID="Register_Result" runat="server" Text="" style="color:red;"></asp:Label><br /></p>
			    	</fieldset>
			      	</form>
			    </div>
			</div>
		</div>
	</div>
</div>
</asp:Content>

