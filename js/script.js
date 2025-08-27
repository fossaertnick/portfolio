"use strict";

window.addEventListener("load", initialize);

const sortOrder = ["Age", "Firstname", "Lastname"];
const voices = ["Dan Castellaneta", "Nancy Cartwright", "Hank Azaria"];

let divOverview, slcChoice, slcSort, divDetails, divVoices, divVoiceCharacters;

function initialize() {

  // initialisering DOM
  divOverview = document.querySelector("#overview");
  divDetails = document.querySelector("#details");
  divVoices = document.querySelector("#voices");
  divVoiceCharacters = document.querySelector("#characters");

  slcChoice = document.querySelector("#choise");
  slcSort = document.getElementById("sort-items"); // heb hier 'getElementById' gebruikt gewoon om eens te veranderen

  // eventhandlers
  slcChoice.addEventListener("change", typeSelected);

  // functies
  FillUpSlcType();
  FillUpSlcSort();
  loadPeople();
  LoadVoiceButtons();
}

// core function
function loadPeople() {

  for (const person in members) {

    const divPerson = CreatePerson(person);

    divOverview.appendChild(divPerson);
  }
}
function FillUpSlcType() { // ik ben me ervan bewust dat dit hard gecodeerd staat

  slcChoice[slcChoice.length] = new Option("family", "family");
  slcChoice[slcChoice.length] = new Option("hobby", "hobby");
  slcChoice[slcChoice.length] = new Option("plant", "plant");
  slcChoice[slcChoice.length] = new Option("school", "school");

}
function FillUpSlcSort() { // ik ben me ervan bewust dat dit hard gecodeerd staat

  
  for(const sort in sortOrder){

    slcSort[slcSort.length] = new Option(sortOrder[sort], sortOrder[sort])
  }
}
function LoadVoiceButtons(){

  for(const button in voices){

    const btnVoice = document.createElement("button");
    btnVoice.id = `${voices[button]}`;
    btnVoice.addEventListener("mouseover", changeColors)
    btnVoice.addEventListener("mouseout", resetColor)
    btnVoice.addEventListener("click", showVoices);
    btnVoice.className = "notSelected";
    btnVoice.textContent = voices[button];
    divVoices.appendChild(btnVoice);
  }

}

// supporting functions
function appointRightFace(person) {

  const imgFace = document.createElement("img");
  imgFace.id = members[person].firstname;
  imgFace.className = "characters img";
  imgFace.addEventListener("click", showInfo); // ik heb er een click van gemaakt die in een alert zal komen

  if (members[person].type === "family") {

    imgFace.src = `/img/Family/${members[person].picture}`;
    imgFace.alt = `${person.firstname} ${person.lastname}`;
  }
  else { // members[person].type !== "family" zullen de images uit de andere folder gehaald worden

    imgFace.src = `/img/Other/${members[person].picture}`;
    imgFace.alt = `${person.firstname} ${person.lastname}`;
  }
  return imgFace;
}
function showInfo() {

  const selectedFigureName = this.id;
  const selectedFigure = members.find(e => e.firstname === selectedFigureName);
  const divDetailedInfo = GetInfo(selectedFigure);
  divDetails.innerHTML = "";
  divDetails.appendChild(divDetailedInfo);
}
function GetInfo(selectedFigure) {
  const divDetailedInfo = document.createElement("div");
  divDetailedInfo.className = "details";

  const h4Name = document.createElement("h4");
  h4Name.className = "bg-crimson";
  h4Name.textContent = `${selectedFigure.firstname} ${selectedFigure.lastname}`;

  const h4Age = document.createElement("h4");
  h4Age.className = "bg-deepsky-blue";
  h4Age.textContent = `Age:`

  const parAge = document.createElement("p");
  parAge.textContent = `${selectedFigure.age}`;

  const h4Job = document.createElement("h4");
  h4Job.className = "bg-deepsky-blue";
  h4Job.textContent = `Job:`

  const parJob = document.createElement("p");
  parJob.textContent = `${selectedFigure.job}`;

  const h4Quote = document.createElement("h4");
  h4Quote.className = "bg-deepsky-blue";
  h4Quote.textContent = `Quote:`

  const parQuote = document.createElement("p");
  parQuote.textContent = `${selectedFigure.quote}`;

  const h4Voice = document.createElement("h4");
  h4Voice.className = "bg-deepsky-blue";
  h4Voice.textContent = `Voice:`

  const parVoice = document.createElement("p");
  parVoice.textContent = `${selectedFigure.voice}`;

  divDetailedInfo.appendChild(h4Name);
  divDetailedInfo.appendChild(h4Age);
  divDetailedInfo.appendChild(parAge);
  divDetailedInfo.appendChild(h4Job);
  divDetailedInfo.appendChild(parJob);
  divDetailedInfo.appendChild(h4Quote);
  divDetailedInfo.appendChild(parQuote);
  divDetailedInfo.appendChild(h4Voice);
  divDetailedInfo.appendChild(parVoice);

  return divDetailedInfo;
}
// supporting function FillUpSlcType (and shows character of selected type)
function typeSelected() {

  divDetails.innerHTML = "";
  divVoiceCharacters.innerHTML = "";
  const selectedTypeValue = slcChoice.value

  divOverview.innerHTML = "";
  for (var person in members) {

    if (members[person].type === selectedTypeValue) {

      const divPerson = CreatePerson(person);

      divOverview.appendChild(divPerson);
    }
  }
}
function sortSelected() {

  // MOET NOG UITGEWERKT WORDEN
}
function showVoices(){

  const selectedVoiceId = this.id;
  divVoiceCharacters.innerHTML = "";
  divDetails.innerHTML = "";
  
  for (var person in members) {

    if (members[person].voice === selectedVoiceId) {

      const divPerson = CreatePerson(person)

      divVoiceCharacters.appendChild(divPerson);
    }
  }
}
function changeColors(){
  this.className = "selected";
}
function resetColor(){
    this.className = "notSelected";
}
function CreatePerson(person){

        const divPerson = document.createElement("div");

      const h3Name = document.createElement("h3");
      h3Name.textContent = members[person].firstname;

      const imgFace = appointRightFace(person);

      divPerson.appendChild(h3Name);
      divPerson.appendChild(imgFace);

      return divPerson;
}



