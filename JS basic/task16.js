//1

var browser = prompt("Ваш браузер","");
if(browser == 'IE') {
  alert('О, да у вас IE!');
} else if (browser == 'Chrome'
 || browser == 'Firefox'
 || browser == 'Safari'
 || browser == 'Opera') {
  alert('Да, и эти браузеры мы поддерживаем');
} else {
  alert('Мы надеемся, что и в вашем браузере все ок!');
}

//2
var a = +prompt('a?', '');

switch (a) {
  case 0:
    alert( 0 );
    break;

  case 1:
    alert( 1 );
    break;

  case 2:
  case 3:
    alert( '2,3' );
    break;
}
