<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Change_Password.aspx.cs" Inherits="Change_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <div class="container">
    <div class="row">
		<div class="col-md-4 col-md-offset-4">
    		<div class="panel panel-default">
			  	<div class="panel-heading">
			    	<h3 class="panel-title">Change Password</h3>
			 	</div>
			  	<div class="panel-body">
			    	<form accept-charset="UTF-8" role="form">
                    <fieldset>
                        <!--Email-->
			    	  	<div class="form-group">
                            <asp:TextBox ID="user_cp_email" runat="server" class="form-control" placeholder="Email"/>
                            <asp:RequiredFieldValidator ID="cp_email_validation" runat="server" ControlToValidate="user_cp_email" ErrorMessage="Please fill with your email address!" ForeColor="Red"></asp:RequiredFieldValidator>
                        <!-- Old Password-->
			    		<div class="form-group">
                            <asp:TextBox class="form-control" placeholder="Password" ID="user_cp_oldpass" TextMode="Password" runat="server" />
                            <asp:RequiredFieldValidator ID="cp_oldpass_validation" runat="server" ControlToValidate="user_cp_oldpass" ErrorMessage="Please fill with your old password!" ForeColor="Red"></asp:RequiredFieldValidator>
			    		</div>
                        <!-- New Password-->
			    		<div class="form-group">
                            <asp:TextBox class="form-control" placeholder="Password" ID="user_cp_newpass" TextMode="Password" runat="server" />
                            <asp:RequiredFieldValidator ID="cp_newpass_validation" runat="server" ControlToValidate="user_cp_newpass" ErrorMessage="Please fill with your new password!" ForeColor="Red"></asp:RequiredFieldValidator>
			    		</div>        
                        <!-- Confirm Password-->
			    		<div class="form-group">
                            <asp:TextBox class="form-control" placeholder="Password" ID="user_cp_confpass" TextMode="Password" runat="server" />
                            <asp:RequiredFieldValidator ID="cp_confpass_validation" runat="server" ControlToValidate="user_cp_confpass" ErrorMessage="Please confirm your new password!" ForeColor="Red"></asp:RequiredFieldValidator>
			    		</div>

			    		<!--Register button-->
                        <asp:Button class="btn btn-lg btn-success btn-block" Text="Change Password" runat="server" ID="Change_password_button" OnClick="ChangePassword_button_Click" /><br />
                        <!--Errors literal register-->
                        <p><asp:Label ID="ChangePassword_Result" runat="server" Text="" style="color:red;"></asp:Label><br /></p>
			    	</fieldset>
			      	</form>
			    </div>
			</div>
		</div>
	</div>
</div>
</asp:Content>

