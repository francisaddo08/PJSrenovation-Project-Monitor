
var data = document.getElementById("no-of-projects").textContent;
document.getElementById("no-of-project").innerHTML = data;

var d = 80;
function drawChart() {
    // Define the chart to be drawn.
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Browser');
    data.addColumn('number', 'Percentage');
    data.addRows([
        ['Firefox', 45.0],
        ['IE', d],
        ['Chrome', 12.8],
        ['Safari', data],
        ['Opera', 6.2],
        ['Others', data]
    ]);

    // Set chart options
    var options = { 'title': 'Browser market shares at a specific website, 2014', 'width': 550, 'height': 400 };

    // Instantiate and draw the chart.
    var chart = new google.visualization.PieChart(document.getElementById('container'));
    chart.draw(data, options);
}
google.charts.setOnLoadCallback(drawChart);