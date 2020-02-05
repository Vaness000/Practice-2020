"use strict";
//1
/*
function bind(func, context , /args/) {
  var bindArgs = [].slice.call(arguments, 2); // сохраняет доп. аргументы
  function wrapper() {                        // возвращает обертку
    var args = [].slice.call(arguments);
    var unshiftArgs = bindArgs.concat(args);  // производится карринг
    return func.apply(context, unshiftArgs);  // передает вызов func с контекстом
  }
  return wrapper;
}*/
//2
function f() {
    alert( this );
  }
  
  var user = {
    g: f.bind("Hello")
  }
  
  user.g();//hello
//3
function f1() {
    alert(this.name);
  }
  
  f1 = f1.bind( {name: "Вася"} ).bind( {name: "Петя" } );
  
  f1();//Вася
//4
function sayHi() {
    alert( this.name );
  }
  sayHi.test = 5;
  alert( sayHi.test ); // 5
  
  var bound = sayHi.bind({
    name: "Вася"
  });
  
  alert( bound.test );//undefined. функция SayHi самостоятельный объект и у нее нет свойства test
//5
function ask(question, answer, ok, fail) {
    var result = prompt(question, '');
    if (result.toLowerCase() == answer.toLowerCase()) ok();
    else fail();
  }
  
  var user = {
    login: 'Василий',
    password: '12345',
  
    loginOk: function() {
      alert( this.login + ' вошёл в сайт' );
    },
  
    loginFail: function() {
      alert( this.login + ': ошибка входа' );
    },
  
    checkPassword: function() {
      ask("Ваш пароль?", this.password, this.loginOk.bind(this), this.loginFail.bind(this));
    }
  };
  
  var vasya = user;
  user = null;
  vasya.checkPassword();
  //6
  function ask(question, answer, ok, fail) {
    var result = prompt(question, '');
    if (result.toLowerCase() == answer.toLowerCase()) ok();
    else fail();
  }
  
  var user = {
    login: 'Василий',
    password: '12345',
  
    loginDone: function(result) {
      alert( this.login + (result ? ' вошёл в сайт' : ' ошибка входа') );
    },
  
    checkPassword: function() {
      ask("Ваш пароль?", this.password, this.loginDone.bind(this, true), this.loginDone.bind(this, false));
    }
  };
  
  user.checkPassword();