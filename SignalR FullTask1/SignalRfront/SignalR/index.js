const url = "https://localhost:7001/";
const connection = new signalR.HubConnectionBuilder()
  .withUrl(url + "offers")
  .configureLogging(signalR.LogLevel.Information)
  .build();

async function start() {
  try {
    await connection.start();
    console.log("SignalR connected");

    $.get(url + "api/Offer", function (data, status) {
      $("#offerValue").text("Begin price : " + data + "$");
    });
  } catch (err) {
    console.log(err);
    setTimeout(() => {
      start();
    }, 5000);
  }
}

start();

connection.on("ReceiveConnectInfo", (message) => {
  let element = document.querySelector("#info");
  // element.innerHTML = message;
});

connection.on("ReceiveDisconnectInfo", (message) => {
  let element = document.querySelector("#info2");
  //element.innerHTML = message;
});

connection.on("ReceiveMessage", (message, value) => {
  let element = document.querySelector("#offerResponseValue");
  element.innerHTML = message + " " + value + "$";
});

function IncreaseOffer() {
  var countDownTimer;
  const timer = document.querySelector("#timer");
  const btn = document.querySelector("#button");
  btn.disabled = true;
  var time = 5;
  timer.textContent = `Time : ${time}`;
  countDownTimer = setInterval(() => {
    time--;
    timer.textContent = `Time : ${time}`;
    if (time == 0) {
      clearInterval(countDownTimer);
      timer.textContent = "";
      btn.disabled = false;
    }
  }, 1000);
  const user = document.querySelector("#user");
  $.get(url + "api/Offer/Increase?data=100", function (data, status) {
    $.get(url + "api/Offer", function (data, status) {
      connection.invoke("SendMessage", user.value);
    });
  });
}
