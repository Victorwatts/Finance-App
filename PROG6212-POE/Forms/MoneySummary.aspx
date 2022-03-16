<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneySummary.aspx.cs" Inherits="PROG6212_POE.Forms.MoneySummary" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" rel="stylesheet"/>
    <title></title>
	<style>
	 body {
  background: #384047;
  font-family: sans-serif;
  font-size: 10px;
}

form {
  background: #fff;
  padding: 4em 4em 2em;
  max-width: 800px;
  margin: 50px auto 0;
  box-shadow: 0 0 1em #222;
  border-radius: 2px;
}
form h2 {
  margin: 0 0 50px 0;
  padding: 10px;
  text-align: center;
  font-size: 30px;
  color: #666666;
  border-bottom: solid 1px #e5e5e5;
}
form p {
  margin: 0 0 3em 0;
  position: relative;
}
form input {
  display: block;
  box-sizing: border-box;
  width: 100%;
  outline: none;
  margin: 0;
}
form input[type="Number"],
form input[type="password"] {
  background: #fff;
  border: 1px solid #dbdbdb;
  font-size: 1.6em;
  padding: .8em .5em;
  border-radius: 2px;
}
form input[type="Number"]:focus,
form input[type="password"]:focus {
  background: #fff;
}
form span {
  display: block;
  background: #F9A5A5;
  padding: 2px 5px;
  color: #666;
}
form input[type="submit"] {
  background: rgba(148, 186, 101, 0.7);
  box-shadow: 0 3px 0 0 rgba(123, 163, 73, 0.7);
  border-radius: 2px;
  border: none;
  color: #fff;
  cursor: pointer;
  display: block;
  font-size: 2em;
  line-height: 1.6em;
  margin: 2em 0 0;
  outline: none;
  padding: .8em 0;
  text-shadow: 0 1px #68B25B;
}
form input[type="submit"]:hover {
  background: #94af65;
  text-shadow: 0 1px 3px rgba(70, 93, 41, 0.7);
}
form label {
  position: absolute;
  left: 8px;
  top: 12px;
  color: #999;
  font-size: 16px;
  display: inline-block;
  padding: 4px 10px;
  font-weight: 400;
  background-color: rgba(255, 255, 255, 0);
  -moz-transition: color 0.3s, top 0.3s, background-color 0.8s;
  -o-transition: color 0.3s, top 0.3s, background-color 0.8s;
  -webkit-transition: color 0.3s, top 0.3s, background-color 0.8s;
  transition: color 0.3s, top 0.3s, background-color 0.8s;
}
form label.floatLabel {
  top: -11px;
  background-color: rgba(255, 255, 255, 0.8);
  font-size: 14px;
}


/* Add a right margin to each icon */
.fa {
  margin-left: -12px;
  margin-right: 8px;
}
   

	    .auto-style1 {
            position: relative;
            width: 800px;
            left: 0px;
            top: 0px;
            margin: 30px auto;
            text-align:center;
        }
   

.topnav {
  overflow: hidden;
  background-color: #333;
}

.topnav a {
  float: left;
  color: #f2f2f2;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  font-size: 17px;
}

.topnav a:hover {
  background-color: #ddd;
  color: black;
}

.topnav a.active {
  background: rgba(148, 186, 101, 0.7);
  color: white;
}
.Gridview
{
font-family:Verdana;
font-size:10pt;
font-weight:normal;
color:black;
}


.center {
  margin: auto;
  width: 20%;
  border: 2px solid black;
  padding: 20px;
  text-align:center;
}
	</style>

</head>

	
<body>
	  <div class="topnav">
		   <a href="Logout.aspx" class="btn btn-default btn-flat" style="background-color:#ff5454">Sign out</a>
         <a>User: <asp:Label ID="Label1" runat="server" Text=""></asp:Label></a>
		<a class="active">Expenditure Summary</a>
		  </div>
    <form id="form1" runat="server">
        <h2>Money Left After All Deductions</h2>
		
        <asp:GridView ID="GridMoneyLeft" runat="server" Font-Size="15pt"></asp:GridView>
        <br />
        
        <asp:Button ID="ViewMore" runat="server" Text="View Details" type="submit" OnClick="ViewMore_Click" />

         <asp:Button ID="GoToSavings" runat="server" Text="Go To Savings" type="submit" OnClick="GoToSavings_Click" />

        <asp:Button ID="submit" runat="server" Text="Logout" OnClick="Submit_Click" type="submit" />

        <asp:Panel ID="pnlAlertBox" Visible="false" runat="server" style="position:absolute; top:10%; left:25%; width:50%;"> 
        <div class="auto-style1">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">Detailed Monthly Expenditure</h4>
                </div>
                <div class="modal-body" style="font-size:180%;">
					<asp:GridView ID="GridIncome" runat="server"></asp:GridView>
        <br />
        <asp:GridView ID="GridExpenses" runat="server"></asp:GridView>
        <br />
        <asp:GridView ID="GridHomeLoan" runat="server"></asp:GridView>
        <br />
        <asp:GridView ID="GridRent" runat="server"></asp:GridView>
        <br />
        <asp:GridView ID="GridVehicleLoan" runat="server"></asp:GridView>
        <br />
						
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnOk" runat="server" CssClass="btn btn-primary" Text="Ok" OnClick="btnOk_Click" />
                </div>
            </div>
        </div>
</asp:Panel>

    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
<!-- Include all compiled plugins (below), or include individual files as needed -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
	
</body>
</html>


