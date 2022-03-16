<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterForm.aspx.cs" Inherits="PROG6212_POE.Forms.RegisterForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
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
  max-width: 400px;
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
form input[type="text"],
form input[type="password"] {
  background: #fff;
  border: 1px solid #dbdbdb;
  font-size: 1.6em;
  padding: .8em .5em;
  border-radius: 2px;
}
form input[type="text"]:focus,
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

	</style>

</head>
<body>
    <form id="form1" runat="server" action="#" method="post">
        <h2>Sign Up</h2>
		<p>
			<label for="Username" class="floatLabel">Username</label>
			<asp:TextBox id="Username" name="Username" type="text" runat="server"/>
		</p>
		<p>
			<label for="password" class="floatLabel">Password</label>
			<asp:TextBox id="password" name="password" type="password" runat="server"></asp:TextBox>
			<%--<asp:CheckBox ID="CheckBox1" onclick="myFunction()" runat="server" />Show Password
			<script>
                function myFunction() {
                    var x = document.getElementById("password");
                    if (x.type === "password") {
                        x.type = "text";
                    } else {
                        x.type = "password";
                    }
                }
            </script>--%>
			<span style=" display:none" runat="server"><asp:label id="LabelP" runat="server" Text="Enter a password longer than 8 characters"/></span>
		</p>
		
		<p>
			<label for="confirm_password" class="floatLabel">Confirm Password</label>
			<asp:TextBox id="confirm_password" name="confirm_password" type="password" runat="server"/>
           <%-- <input type="checkbox" onclick="myFunction2()" />Show Password
			<script>
                function myFunction2() {
                    var x = document.getElementById("confirm_password");
                    if (x.type === "password") {
                        x.type = "text";
                    } else {
                        x.type = "password";
                    }
                }
            </script>--%>
			<span style=" display:none" runat="server"><asp:label id="LabelCP" runat="server" Text="Your passwords do not match"/></span>
		</p>
		
			<asp:Button ID="submit" runat="server" Text="Create My Account" OnClick="Submit_Click" type="submit" />

        <div>
            <asp:label id="LabelAlert" runat="server" Text="" Visible="false"/>
        </div>

        <div><asp:Button ID="GoToLogin" runat="server" Text="Go To Login" OnClick="GoToLogin_Click"/></div>
       
        
    </form>
    <script src="https://static.codepen.io/assets/common/stopExecutionOnTimeout-157cd5b220a5c80d4ff8e0e70ac069bffd87a61252088146915e8726e5d9f147.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script>
        var $password = $("#password");
        var $confirmPassword = $("#confirm_password");

        function isPasswordValid() {
            return $password.val().length > 8;
        }

        function arePasswordsMatching() {
            return $password.val() === $confirmPassword.val();
        }


        function passwordEvent() {
            //Find out if password is valid  
            if (isPasswordValid()) {
                //Hide hint if valid
                $password.next().hide();
            } else {
                //else show hint
                $password.next().show();
            }
        }

        function confirmPasswordEvent() {
            //Find out if password and confirmation match
            if (arePasswordsMatching()) {
                //Hide hint if match
                $confirmPassword.next().hide();
            } else {
                //else show hint 
                $confirmPassword.next().show();
            }
        }

        $password.focus(passwordEvent).keyup(passwordEvent).keyup(confirmPasswordEvent);

        //When event happens on confirmation input
        $confirmPassword.focus(confirmPasswordEvent).keyup(confirmPasswordEvent);
    </script>
   
</body>
</html>
