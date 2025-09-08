const {add} = require("./math.js");

//변수 선언
var num = 42;
var name = "TOM";
let isStudent = true;

console.log(add(num, num));

//배열
let color = ["red" , "green" , "blue"];

//객체
let person = { name : "Alice ", age : 30 };

//함수
function greet(name)
{
    console.log("Hello " + name + " ! " );
}

//함수 호출
greet(person.name);

//조건문
if(num > 30)
{
    console.log("Number is greater than 30");
}
else
{
    console.log("Number is lower than 30");
}

//반복문
for(var i = 0; i < 5; i++)
{
    console.log(i);
}

//비동기 콜백
setTimeout(() => {
    console.log("Delayed Message 1");
}, 1000);

setTimeout(() => {
    console.log("Delayed Message 2");
}, 750);

setTimeout(() => {
    console.log("Delayed Message 3");
}, 2000);

setTimeout(() => {
    console.log("Delayed Message 4");
}, 500);