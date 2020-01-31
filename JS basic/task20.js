function g() { return 1; }

alert(g);//выдаст function g() { return 1; } потому что вызвана не правильно
(function g1() { return 1; });

alert(g1);//ошибка так как есть скобки
