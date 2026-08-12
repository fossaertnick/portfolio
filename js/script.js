"use strict";

let divOverviewFaces, divDetailsCharacter, divVoices, divVoiceCharacters, slcLocation, slcSort;
const locationTypes = [
  {
    Name: "family",
    Value: 1
  },
  {
    Name: "school",
    Value: 2
  },
  {
    Name: "plant",
    Value: 3
  },
  {
    Name: "hobby",
    Value: 4
  },
];
const sortingTypes = [
  {
    Name: "age",
    Value: 1
  },
  {
    Name: "firstname",
    Value: 2
  },
  {
    Name: "lastname",
    Value: 3
  }
];
const voiceActors = [
  {
    Name: "Dan Castellaneta",
  },
  {
    Name: "Nancy Cartwright",
  },
  {
    Name: "Hank Azaria",
  }
];

window.addEventListener("load", initialize);

function initialize() {

  // initialisering DOM
  divOverviewFaces = document.getElementById("overview");
  divDetailsCharacter = document.getElementById("details");
  divVoices = document.getElementById("voices");
  divVoiceCharacters = document.getElementById("characters");
  slcLocation = document.querySelector("#choise");
  slcSort = document.querySelector("#sort-items");

  // eventhandlers
  slcLocation.addEventListener("change", () => basedOnLocationAndSort(slcLocation[slcLocation.selectedIndex], slcSort[slcSort.selectedIndex]));
  slcSort.addEventListener("change", () => basedOnLocationAndSort(slcLocation[slcLocation.selectedIndex], slcSort[slcSort.selectedIndex]));

  // functies
  loadWaysOfSorting();
  loadPeople();
}

// core functions
function loadWaysOfSorting() {
  locationTypes.forEach(type => slcLocation.options[slcLocation.length] = new Option(`${type.Name}`, `${type.Value}`));
  sortingTypes.forEach(type => slcSort.options[slcSort.length] = new Option(`${type.Name}`, `${type.Value}`))
  voiceActors.forEach(actor => {
    let btnName = document.createElement("button");
    btnName.classList.add("notSelected");
    btnName.addEventListener("click", () => {
      btnName.classList.remove("notSelected");
      btnName.classList.add("selected");
      showVoicedCharacters(actor)
    });
    btnName.addEventListener("dblclick", () => {
      btnName.classList.remove("selected");
      btnName.classList.add("notSelected");
      divVoiceCharacters.innerHTML = "";
    });

    btnName.textContent = actor.Name;
    divVoices.appendChild(btnName);
  })
}
function loadPeople() {
  for (let i = 0; i < members.length; i++) {
    createSpecificOne(members[i])
  }
}

// supporting functions
function createSpecificOne(character) {
  const simpCharacter = document.createElement("div");
  simpCharacter.addEventListener("click", () => showDetailsCharacter(character));

  let characterFace = document.createElement("img");

  let characterName = document.createElement("h3");
  characterName.textContent = character.firstname;

  characterFace = getTheCharactersImage(character);

  simpCharacter.append(characterName, characterFace);

  divOverviewFaces.appendChild(simpCharacter);
}
function getTheCharactersImage(character) {
  let image = document.createElement("img");

  if (character.type === "family") {
    image.src = `img/Family/${character.picture}`;
    image.title = `${character.lastname} ${character.firstname}`
  }
  else // (members[character].type === "hobby" || members[character].type === "school" || members[character].type === "plant")
  {
    image.src = `img/Other/${character.picture}`;
    image.title = `${character.lastname} ${character.firstname}`
  }

  return image;
}
function showDetailsCharacter(character) {
  divVoiceCharacters.innerHTML = "";
  divDetailsCharacter.innerHTML = "";

  let details = document.createElement("h3");
  details.classList.add("bg-crimson")
  details.textContent = "Details";

  let titleName = document.createElement("h3");
  titleName.textContent = "Name";
  let name = document.createElement("p");
  name.classList.add("details")
  name.textContent = `${character.firstname}`;

  let titleAge = document.createElement("h3");
  titleAge.textContent = "Age";
  let age = document.createElement("p");
  age.classList.add("details")
  age.textContent = `${character.age}`;

  let titleJob = document.createElement("h3");
  titleJob.textContent = "Job";
  let job = document.createElement("p");
  job.classList.add("details")
  job.textContent = `${character.job}`;

  let titleQuote = document.createElement("h3");
  titleQuote.textContent = "Quote";
  let quote = document.createElement("p");
  quote.classList.add("details")


  if (character.favorite_quotes === undefined) {
    quote.textContent = "no quote here.";
  }
  else {
    quote.textContent = pickRandomQuote(character.favorite_quotes);
  }

  let titleVoice = document.createElement("h3");
  titleVoice.textContent = "Voice";
  let voice = document.createElement("p");
  voice.classList.add("details")
  voice.textContent = `${character.voice}`;

  divDetailsCharacter.append(details, titleName, name, titleAge, age, titleJob, job, titleQuote, quote, titleVoice, voice)
}
function pickRandomQuote(characterQuotes) {
  const quoteAtHand = characterQuotes[Math.floor(Math.random() * characterQuotes.length)];
  return quoteAtHand;
}
function showVoicedCharacters(voiceActor) {
  divDetailsCharacter.innerHTML = "";
  divVoiceCharacters.innerHTML = "";

  const simpCharacter = document.createElement("div");
  simpCharacter.classList.add("characters");
  members.forEach(character => {
    if (character.voice === voiceActor.Name) 
    {
        let characterFace = document.createElement("img");

        characterFace = getTheCharactersImage(character);
        simpCharacter.appendChild(characterFace);
    }
  })
  divVoiceCharacters.appendChild(simpCharacter);
}
function basedOnLocationAndSort(location, sort) {
  divVoiceCharacters.innerHTML = "";
  divOverviewFaces.innerHTML = "";
  let locationCharacters = [];

  members.forEach(character => {
    if (location.text === "All") {
      locationCharacters.push(character);
    }
    else if (character.type === location.text) {
      locationCharacters.push(character);
    }
  });

  if (sort.text === "age") {
    locationCharacters.sort((a, b) => b.age - a.age);
  }
  else if (sort.text === "firstname") {
    locationCharacters.sort((a, b) => a.firstname.localeCompare(b.firstname));
  }
  else if (sort.text === "lastname") {
    locationCharacters.sort((a, b) => a.lastname.localeCompare(b.lastname));
  }

  locationCharacters.forEach(character => { createSpecificOne(character) });
}





