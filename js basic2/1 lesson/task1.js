//1
if ("a" in window) {
    var a = 1;
  }
  alert( a );//1
//2
/*if ("a1" in window) {
    a1 = 1;
  }
  alert( a1 );// ошибка потому что переменная а1 не объявлена*/
//3
if ("a" in window) {
    a = 1;
  }
  var a;
  
  alert( a ); //1 так как а объявлена
  