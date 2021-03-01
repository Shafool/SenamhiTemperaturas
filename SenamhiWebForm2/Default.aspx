<%@ Page Title="Senamhi Temperaturas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SenamhiWebForm2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = google.visualization.arrayToDataTable(<%=obtenerDatos()%>);
            console.log(<%=obtenerDatos()%>);

            var options = {
                title: 'Historial de temperaturas',
                xAxis: { title: 'Fecha' },
                vAxis: { title: 'Temperatura' },
                colors: ['#E49307', '#53A8FB'],
                legend: { position: 'bottom' }
            };

            //var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));
            var chart = new google.visualization.AreaChart(document.getElementById('curve_chart'));
            chart.draw(data, options);
        }
    </script>

    <div class="row">
            <h2>Buscar</h2>
            <div class="form-inline">
              <div class="form-group mx-sm-3 mb-2">
                <asp:TextBox runat="server" class="form-control" id="tbBusqueda" placeholder="Busca entre las localidades: Cusco, Arequipa, Lima..."/>
              </div>
              <asp:Button class="btn btn-primary mb-2" runat="server" Text="Buscar" OnClick="btBuscar_Click"></asp:Button>
            </div>
    </div>

    <!--
    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>
    -->

    <div class="row">
        
        <div class=" jumbotron col-md-3" style="margin-right: 10px">
            <div class="container">
                <asp:Label ID="lbCiudad" runat="server" class="h2 " Text="Ciudad"></asp:Label>
                <div class="row">
                    <asp:Image ID="imagenEstado" runat="server" width="200px" height="200px"/>
                </div>
                <asp:Label ID="lbDesc" runat="server" Text="Sin descripcion"></asp:Label>
                <div class="row">
                    <asp:Label ID="lbTempHoy" runat="server" Text="0.0 °C"  class="h3"></asp:Label>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col">
                            <asp:Label ID="lbTempMinMax" runat="server" Text="0.0 °C"></asp:Label>
                        </div>

                    </div>
                    <div class="row">
                </div>
                    
                </div>
            </div>
        </div>

        <div class="col-md-4">
            <asp:GridView ID="GridViewRegistros" class="table" runat="server" />
            <div id="curve_chart" style="width: 900px; height: 500px"></div>
        </div>
    </div>

</asp:Content>
