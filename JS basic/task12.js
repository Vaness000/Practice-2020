//1
if ("0") {
  alert( 'Привет' );
};
//выведется потому что "0" не пустая строка и приведется к true
//2
var message = prompt("Каково «официальное» название JavaScript?","");
if(message == "ECMAScript"){
  alert("Верно!")
}
else{
  alert("Не знаете? «ECMAScript»!")
}
//3
var number = prompt("Введите число",0);
if(number>0){
  alert(1);
}else if(number==0){
  alert(0);
}
else {
  alert(-1);
}
//4
var user = prompt("Кто пришел","");
if(user == "Админ"){
  var password = prompt("Введите пароль","");
  if(password == "Чёрный Властелин"){
    alert("Добро пожаловать!");
  }
  else if(password == null){
    alert("Вход отменен");
  }
  else
  {
      alert("Пароль неверен");
    }
  }else if(user == null){
    alert("Вход отменен");
  }else{
    alert("Я вас не знаю!");
}
//5
var a = 2;
var b = 1;
var result = (a+b>4) ? alert("Много"):alert("Мало");
//6
var login = prompt("Логин","");
var message = (login == 'Вася') ? alert('Привет') :
  (login == 'Директор') ? alert('Здравствуйте') :
  (login == '') ? alert('Нет логина') :
  alert('');
