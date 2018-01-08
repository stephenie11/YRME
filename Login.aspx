<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">

<div class="container">
    <div class="row">
		<div class="col-md-4 col-md-offset-4">
    		<div class="panel panel-default">
			  	<div class="panel-heading">
			    	<h3 class="panel-title">Please sign in</h3>
			 	</div>
			  	<div class="panel-body">
			    	<form accept-charset="UTF-8" role="form" runat="server">
                    <fieldset>
			    	  	<div class="form-group">
			    		    <!-- <input class="form-control" placeholder="E-mail" name="email" type="text"> -->
                            <asp:TextBox ID="user_login_email" runat="server" class="form-control" placeholder="E-mail"/>
                            <asp:RequiredFieldValidator ID="login_e_validation" runat="server" ControlToValidate="user_login_email" ErrorMessage="Please fill with your email address!" ForeColor="Red"></asp:RequiredFieldValidator>
			    		</div>
			    		<div class="form-group">
			    			<!-- <input class="form-control" placeholder="Password" name="password" type="password" value=""> -->
                            <asp:TextBox class="form-control" placeholder="Password" ID="user_login_password" TextMode="Password" runat="server" />
                            <asp:RequiredFieldValidator ID="login_p_validation" runat="server" ControlToValidate="user_login_password" ErrorMessage="Please fill with your password!" ForeColor="Red"></asp:RequiredFieldValidator>
			    		</div>
			    		<!-- <input class="btn btn-lg btn-success btn-block" type="submit" value="Login"> -->
                        <asp:Button class="btn btn-lg btn-success btn-block" Text="Login" runat="server" ID="Login_button" OnClick="Login_button_Click" />
                        <br />
                            <p>
                                 <asp:Label ID="Result" runat="server" Text="" style="color:red;"></asp:Label>
                                <br />

                            </p>
			    	</fieldset>
			      	</form>
			    </div>
			</div>
		</div>
	</div>
</div>
</asp:Content>

