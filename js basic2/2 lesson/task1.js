"use strict"
var arr = ["a", "b"];

arr.push(function() {
  alert( this );
})
arr[2]();//выведутся все элементы данного массива включая функцию
//2
var obj = {
    go: function() { alert(this) }
  };
(obj.go)();//ошибка пропущена точка с запятой
//3
var obj1, method;

obj1 = {
  go: function() { alert(this); }
};

obj1.go();            // в контексте объекта

(obj1.go)();          // то же самое

(method = obj1.go)();      // неправильный вызов

(obj1.go || obj.stop)(); // неправильный вызов
//4
var user = {
    firstName: "Василий",
  
    export: this
  };
  
  alert( user.export.firstName );//undefined, потому что тут this = window
//5
var name1 = "";

var user1 = {
  name1: "Василий",

  export: function() {
    return this;
  }

};

alert( user1.export().name1 );//Василий, строка return this вернет нужный объект
//6
var name2 = "";

var user2 = {
  name2: "Василий",

  export: function() {
    return {
      value: this
    };
  }

};

alert( user2.export().value.name2 );//Василий работает примерно так же как в предыдущем задании, 
//value станет равным текущему объекту
//7
var calculator = {
    sum: function(){
        return this.a+this.b;
    },
    mul: function(){
        return this.a*this.b;
    },
    read: function(){
        this.a = +prompt('a',0);
        this.b = +prompt('b',0);
    }
}
calculator.read();
alert( calculator.sum() );
alert( calculator.mul() );
//8
var ladder = {
    step: 0,
    up: function() { // вверх по лестнице
      this.step++;
      return this;//нужно добавить в конце каждого метода чтобы возвращать объект и обращаться к его методам
    },
    down: function() { // вниз по лестнице
      this.step--;
      return this;
    },
    showStep: function() { // вывести текущую ступеньку
      alert( this.step );
      return this;
    }
  };
ladder.up();
ladder.up();
ladder.down();
ladder.showStep();
ladder.up().up().down().up().down().showStep();

