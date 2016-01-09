﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="UnicoTesting.Login1" %>

<%@ Register Src="~/Login.ascx" TagPrefix="uc1" TagName="Login" %>




<!DOCTYPE html>
<html>

<head>

  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">

  
        <!-- Hammer reload -->
          <script>
              setInterval(function () {
                  try {
                      if (typeof ws != 'undefined' && ws.readyState == 1) { return true; }
                      ws = new WebSocket('ws://' + (location.host || 'localhost').split(':')[0] + ':35353')
                      ws.onopen = function () { ws.onclose = function () { document.location.reload() } }
                      ws.onmessage = function () {
                          var links = document.getElementsByTagName('link');
                          for (var i = 0; i < links.length; i++) {
                              var link = links[i];
                              if (link.rel === 'stylesheet' && !link.href.match(/typekit/)) {
                                  href = link.href.replace(/((&|\?)hammer=)[^&]+/, '');
                                  link.href = href + (href.indexOf('?') >= 0 ? '&' : '?') + 'hammer=' + (new Date().valueOf());
                              }
                          }
                      }
                  } catch (e) { }
              }, 1000)
          </script>
        <!-- /Hammer reload -->
      

  <link rel='stylesheet' href='assets/css/fullcalendar.css'>
<link rel='stylesheet' href='assets/css/datatables/datatables.css'>
<link rel='stylesheet' href='assets/css/datatables/bootstrap.datatables.css'>
<link rel='stylesheet' href='assets/scss/chosen.css'>
<link rel='stylesheet' href='assets/scss/font-awesome/font-awesome.css'>
<link rel='stylesheet' href='assets/css/app.css'>

  <link href='http://fonts.googleapis.com/css?family=Oswald:300,400,700|Open+Sans:400,700,300' rel='stylesheet' type='text/css'>

  <link href="assets/favicon.ico" rel="shortcut icon">
  <link href="assets/apple-touch-icon.png" rel="apple-touch-icon">
  <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!--[if lt IE 9]>
    @javascript html5shiv respond.min
  <![endif]-->

  <title>Unico Sybase ETL Interface | Login Page</title>

</head>

<body>

  <script>
      (function (i, s, o, g, r, a, m) {
          i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
              (i[r].q = i[r].q || []).push(arguments)
          }, i[r].l = 1 * new Date(); a = s.createElement(o),
          m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
      })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

      ga('create', 'UA-42863888-3', 'pinsupreme.com');
      ga('send', 'pageview');

  </script>

<div class="all-wrapper no-menu-wrapper">
  <div class="login-logo-w">
    <a href="index.html" class="logo">
       <img src="assets/images/small_size.gif"/>
    <span>UNICO SYBASE ETL INTERFACE</span>
    <br/>
    Powered By C2G Consulting
  </a>
  </div>
  <div class="row">
    <div class="col-md-4 col-md-offset-4">

      <div class="content-wrapper bold-shadow">
      
          <form id="form1" runat="server">
    <div>
        <uc1:Login runat="server" ID="Login" />
    </div>
    </form>
       </div>
        </div>
      </div>
    </div>
  </div>





<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
<script src='assets/js/jquery.sparkline.min.js'></script>
<script src='assets/js/bootstrap/tab.js'></script>
<script src='assets/js/bootstrap/dropdown.js'></script>
<script src='assets/js/bootstrap/collapse.js'></script>
<script src='assets/js/bootstrap/transition.js'></script>
<script src='assets/js/bootstrap/tooltip.js'></script>
<script src='assets/js/jquery.knob.js'></script>
<script src='assets/js/fullcalendar.min.js'></script>
<script src='assets/js/datatables/datatables.min.js'></script>
<script src='assets/js/chosen.jquery.min.js'></script>
<script src='assets/js/datatables/bootstrap.datatables.js'></script>
<script src='assets/js/raphael-min.js'></script>
<script src='assets/js/morris-0.4.3.min.js'></script>
<script src='assets/js/for_pages/color_settings.js'></script>
<script src='assets/js/application.js'></script>

</body>

</html>
