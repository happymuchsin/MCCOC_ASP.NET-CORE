var year = [];

$(document).ready(function () {
    am4core.ready(function () {
        am4core.useTheme(am4themes_animated);

        var chart = am4core.create("receiptdivline", am4charts.XYChart);

        chart.dataSource.url = "/Dashboard/LoadReceipt";
        chart.dataSource.parser = new am4core.JSONParser();

        var dateAxis = chart.xAxes.push(new am4charts.DateAxis());

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

        var series = chart.series.push(new am4charts.LineSeries());
        series.dataFields.valueY = "totalPrice";
        series.dataFields.dateX = "orderDate";
        series.strokeWidth = 1;
        series.minBulletDistance = 10;
        series.tooltipText = "{valueY}";
        series.fillOpacity = 0.1;
        series.tooltip.pointerOrientation = "vertical";
        series.tooltip.background.cornerRadius = 20;
        series.tooltip.background.fillOpacity = 0.5;
        series.tooltip.label.padding(12, 12, 12, 12)

        var seriesRange = dateAxis.createSeriesRange(series);
        seriesRange.contents.strokeDasharray = "2,3";
        seriesRange.contents.stroke = chart.colors.getIndex(8);
        seriesRange.contents.strokeWidth = 1;

        var pattern = new am4core.LinePattern();
        pattern.rotation = -45;
        pattern.stroke = seriesRange.contents.stroke;
        pattern.width = 1000;
        pattern.height = 1000;
        pattern.gap = 6;
        seriesRange.contents.fill = pattern;
        seriesRange.contents.fillOpacity = 0.5;

        chart.scrollbarY = new am4core.Scrollbar();

        var scrollbarX = new am4charts.XYChartScrollbar();
        scrollbarX.series.push(series);
        chart.scrollbarX = scrollbarX;
        chart.scrollbarX.parent = chart.bottomAxesContainer;
    });
})