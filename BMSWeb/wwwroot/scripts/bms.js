var Top5Chart = echarts.init(document.getElementById('Top5Chart'));
var PartChart = echarts.init(document.getElementById('PartChart'));
var Bottom5Chart = echarts.init(document.getElementById('Bottom5Chart'));
Top5Chart.showLoading();
PartChart.showLoading();
Bottom5Chart.showLoading();

$.getJSON(window.location.origin + '/api/Inventory', function (data) {
  var Top5PartsData = [];
  var Bottom5PartsData = [];
  var Top10PartsData = [];
  var Top10QuantityData = [];
  var Top10CapacityData = [];
  var k = 0;
  Top5Chart.hideLoading();
  PartChart.hideLoading();
  Bottom5Chart.hideLoading();
  if (data.length < 1) {
    return;
  }
  $.each(data, function (d, i) {
    if (k < 5) {
      Top5PartsData.push({
        value: i.totalQuantity,
        name: i.part.name
      });
    }
    if (k < 10) {
      Top10PartsData.push(i.part.name);
      Top10QuantityData.push(i.totalQuantity);
      Top10CapacityData.push(i.inventoryLocations[0].binLocation.capacity);
    }
    k++;
    if (k > 9)
      return false;
  })
  for (j = data.length-1; j > data.length - 5; j--) {
    Bottom5PartsData.push({
      value: data[j].totalQuantity,
      name: data[j].part.name
    })
  };
  

  // specify chart configuration item and data
  top5option = {
    title: {
      text: '',
      left: 'center'
    },
    tooltip: {
      trigger: 'item',
      formatter: "{a} <br/>{b} : {c} ({d}%)"
    },
    legend: {
      orient: 'vertical',
      x: 'left',
      data: Top5PartsData.name
    },
    series: [{
      type: 'pie',
      radius: '60%',
      center: ['50%', '50%'],
      data: Top5PartsData
    }]
  };










  //var posList = [
  //  'left', 'right', 'top', 'bottom',
  //  'inside',
  //  'insideTop', 'insideLeft', 'insideRight', 'insideBottom',
  //  'insideTopLeft', 'insideTopRight', 'insideBottomLeft', 'insideBottomRight'
  //];

  //configParameters = {
  //  rotate: {
  //    min: -90,
  //    max: 90
  //  },
  //  align: {
  //    options: {
  //      left: 'left',
  //      center: 'center',
  //      right: 'right'
  //    }
  //  },
  //  verticalAlign: {
  //    options: {
  //      top: 'top',
  //      middle: 'middle',
  //      bottom: 'bottom'
  //    }
  //  },
    
  //  distance: {
  //    min: 0,
  //    max: 100
  //  }
  //};

  //config = {
  //  rotate: 90,
  //  align: 'left',
  //  verticalAlign: 'middle',
  //  position: 'insideBottom',
  //  distance: 15,
  //  onChange: function () {
  //    var labelOption = {
  //      normal: {
  //        rotate: config.rotate,
  //        align: config.align,
  //        verticalAlign: config.verticalAlign,
  //        position: config.position,
  //        distance: config.distance
  //      }
  //    };
  //    PartChart.setOption({
  //      series: [{
  //        label: labelOption
  //      }, {
  //        label: labelOption
  //      }, {
  //        label: labelOption
  //      }, {
  //        label: labelOption
  //      }]
  //    });
  //  }
  //};




  //var labelOption = {
  //  normal: {
  //    show: true,
  //    position: config.position,
  //    distance: config.distance,
  //    align: config.align,
  //    verticalAlign: config.verticalAlign,
  //    rotate: config.rotate,
  //    formatter: '{c}  {name|{a}}',
  //    fontSize: 16,
  //    rich: {
  //      name: {
  //        textBorderColor: '#fff'
  //      }
  //    }
  //  }
  //};


  partoption = {
    title: {
      text: ''
    },
    color: ['#003366', '#006699', '#4cabce', '#e5323e'],
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'shadow'
      }
    },
    legend: {
      data: []
    },
    toolbox: {
      show: true,
      orient: 'vertical',
      left: 'right',
      top: 'center',
      feature: {
        mark: { show: true },
        dataView: { show: true, readOnly: false },
        magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
        restore: { show: true },
        saveAsImage: { show: true }
      }
    },
    calculable: true,
    xAxis: {
      type: 'category',
      axisTick: { show: false },
      data: Top10PartsData
    },
    yAxis: {},
    series: [
      {
        name: 'Quantity',
        barGap: 0,
        type: 'bar',
        label: '',
        data: Top10QuantityData
      },
      {
        name: 'Capacity',
        type: 'bar',
        data: Top10CapacityData
      }]
  };
  bottom5option = {
    title: {
      text: 'ECharts entry example'
    },
    tooltip: {},
    legend: {
      data: ['partNo']
    },
    series: [{
      type: 'pie',
      radius: '60%',
      center: ['50%', '50%'],

      data: Bottom5PartsData
    }]
  };
  // use configuration item and data specified to show chart
  Top5Chart.setOption(top5option);
  PartChart.setOption(partoption);
  Bottom5Chart.setOption(bottom5option);
});
