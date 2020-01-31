//1

var d = new Date(2012, 1, 20, 3, 12);
alert( d );


var date = new Date(2012,0,3);

alert( getWeekDay(date) );
function getWeekDay(date) {
  var days = ['вс', 'пн', 'вт', 'ср', 'чт', 'пт', 'сб'];

  return days[date.getDay()];
}
//2
function getLocalDay(date) {

  var day = date.getDay();

  if (day == 0) {
    day = 7;
  }

  return day;
}

alert( getLocalDay(new Date(2012, 0, 3)) ); // 2
//3
function getDateAgo(date, days) {
  var dateCopy = new Date(date);

  dateCopy.setDate(date.getDate() - days);
  return dateCopy.getDate();
}

var date = new Date(2015, 0, 2);

alert( getDateAgo(date, 1) );
alert( getDateAgo(date, 2) );
alert( getDateAgo(date, 365) );
//4
function getLastDayOfMonth(year, month) {
  var date = new Date(year, month + 1, 0);
  return date.getDate();
}

alert( getLastDayOfMonth(2019, 0) );
alert( getLastDayOfMonth(2019, 1) );
alert( getLastDayOfMonth(2019, 1) );
//5
function getSecondsToday() {
  var now = new Date();
  var today = new Date(now.getFullYear(), now.getMonth(), now.getDate());

  var diff = now - today;
  return Math.floor(diff / 1000);
}
alert( getSecondsToday() );
//6
function getSecondsToTomorrow() {
  var now = new Date();
  var tomorrow = new Date(now.getFullYear(), now.getMonth(), now.getDate()+1);

  var diff = tomorrow - now;
  return Math.round(diff / 1000);
}
alert(getSecondsToTomorrow());
//7
function formatDate(date) {

  var dd = date.getDate();
  if (dd < 10) dd = '0' + dd;

  var mm = date.getMonth() + 1;
  if (mm < 10) mm = '0' + mm;

  var yy = date.getFullYear() % 100;
  if (yy < 10) yy = '0' + yy;

  return dd + '.' + mm + '.' + yy;
}

var d = new Date(2014, 0, 30);
alert( formatDate(d) );
//8
function formatDate(date) {
  var diff = new Date() - date;

  if (diff < 1000) {
    return 'только что';
  }

  var sec = Math.floor(diff / 1000);
  if (sec < 60) {
    return sec + ' сек. назад';
  }

  var min = Math.floor(diff / 60000);
  if (min < 60) {
    return min + ' мин. назад';
  }

  var d = date;
  d = [
    '0' + d.getDate(),
    '0' + (d.getMonth() + 1),
    '' + d.getFullYear(),
    '0' + d.getHours(),
    '0' + d.getMinutes()
  ];

  for (var i = 0; i < d.length; i++) {
    d[i] = d[i].slice(-2);
  }

  return d.slice(0, 3).join('.') + ' ' + d.slice(3).join(':');
}

alert( formatDate(new Date(new Date - 1)) );

alert( formatDate(new Date(new Date - 30 * 1000)) );

alert( formatDate(new Date(new Date - 5 * 60 * 1000)) );

alert( formatDate(new Date(new Date - 86400 * 1000)) );
