<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Status</title>
    </head>
    <body>
        <form id="HtmlForm" runat="server">
            <div>
                smtpUser: <%= ConfigurationManager.AppSettings["smtpUser"] %><br/>
                smtpPassword: <%= ConfigurationManager.AppSettings["smtpPassword"] %>
            </div>
        </form>
    </body>
</html>
