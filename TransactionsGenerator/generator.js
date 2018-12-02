const fs = require('fs');
const path = require('path');

const STATE_CONST =  {
	man: "чоловіча",
	woman: "жіноча"
}

const AGE_CONST = {
	stage1: "15-17",
	stage2: "18-21",
	stage3: "22-25",
	stage4: "26-30",
	stage5: "31-35",
	stage6: "36-40",
	stage7: "41-45",
	stage8: "46-50"
}

const ALCOHOL_CONST = {
	stage1: "alco 0",
	stage2: "alco 0.2-0.5",
	stage3: "alco 0.6-0.9",
	stage4: "alco 1.0-1.5",
	stage5: "alco 1.6-2.8"
}

const TIME_CONST = {
	stage1: "ранок",
	stage2: "обід",
	stage3: "вечір",
	stage4: "ніч"
}
const SEASON_CONST = {
	summer: "літо",
	autumn: "осінь",
	winter: "зима",
	spring: "весна",
}

const RESIDENCE = {
	city: "місто",
	village: "село"
}

const EDUCATION = {
	hight: "вища",
	medium: "середня",
	low: "нижча"
}

const YEAR_RELEASE = {
	stage1: "1-5",
	stage2: "6-10",
	stage3: "11-15",
	stage4: "16-20"
}

const OWNER = {
	stage1: "власник 1",
	stage2: "власник 2",
	stage3: "власник 3",
	stage4: "власник 4+"
}

const COUNT_OF_PASSANGER = {
	stage1: "пасажир 0",
	stage2: "пасажири 1",
	stage3: "пасажири 2",
	stage4: "пасажири 3",
	stage5: "пасажири 4",
	stage6: "пасажири 4+",
}

const COLOR = {
	white: "білий",
	black: "чорний",
	red: "червоний",
	green: "зелений",
	yellow: "жовтий",
}

fs.writeFile(path.join(__dirname, 'transactions.txt'), generate(), (err) => {
       if (err) {
            console.error(err.message);
        }
   });
	
	function generate() {
		const count = 1000000;
		let result = "";

		for (var i = 0; i < count; i++) {
			result += `"${getState()}"`;
			result += `"${getAge()}"`;
			result += `"${getAlco()}"`;
			result += `"${getTime()}"`;
			result += `"${getColor()}"`;
			result += `"${getEducation()}"`;
			result += `"${getCountOfPassanger()}"`;
			result += `"${getOwner()}"`;
			result += `"${getYearRelease()}"`;
			result += `"${getResidence()}"`;
			result += `"${getSeason()}"\r\n`;
		}

		return result;
	}
	
	function getState() {
		let number = Math.random() * 100;
		if (number <= 65) {
			return STATE_CONST.man;
		} else {
			return STATE_CONST.woman;
		}
	};
	
	function getAge() {
		let number = Math.random() * 100;
		
		if (number <= 7) {
			return AGE_CONST.stage1;
		} else if (number <= 30) {
			return AGE_CONST.stage2;
		} else if (number <= 41) {
			return AGE_CONST.stage3;
		} else if (number <= 46) {
			return AGE_CONST.stage4;
		} else if (number <= 58) {
			return AGE_CONST.stage5;
		} else if (number <= 70) {
			return AGE_CONST.stage6;
		} else if (number <= 85) {
			return AGE_CONST.stage7;
		} else {
			return AGE_CONST.stage8
		}
	}

	function getAlco() {
		let number = Math.random() * 100;
		
		if (number <= 50) {
			return ALCOHOL_CONST.stage5;
		} else if (number <= 70) {
			return ALCOHOL_CONST.stage4;
		} else if (number <= 85) {
			return ALCOHOL_CONST.stage3;
		} else if (number <= 93) {
			return ALCOHOL_CONST.stage2;
		} else {
			return ALCOHOL_CONST.stage1
		}
	}


	function getTime() {
		let number = Math.random() * 100;
		
		if (number <= 30) {
			return TIME_CONST.stage1;
		} else if (number <= 60) {
			return TIME_CONST.stage2;
		} else if (number <= 80) {
			return TIME_CONST.stage3;
		} else {
			return TIME_CONST.stage4
		}
	}


	function getSeason() {
		let number = Math.random() * 100;
		
		if (number <= 42) {
			return SEASON_CONST.winter;
		} else if (number <= 55) {
			return SEASON_CONST.summer;
		} else if (number <= 80) {
			return SEASON_CONST.autumn;
		} else {
			return SEASON_CONST.spring
		}
	}


	function getResidence() {
		let number = Math.random() * 100;
		
		if (number <= 65) {
			return RESIDENCE.city;
		} else {
			return RESIDENCE.village
		}
	}

	function getEducation() {
		let number = Math.random() * 100;
		
		if (number <= 20) {
			return EDUCATION.hight;
		} else if (number <= 50) {
			return EDUCATION.medium
		} else {
			return EDUCATION.low;
		}
	}

	function getYearRelease() {
		let number = Math.random() * 100;
		
		if (number <= 15) {
			return YEAR_RELEASE.stage1;
		} else if (number <= 35) {
			return YEAR_RELEASE.stage2;
		} else if (number <= 70) {
			return YEAR_RELEASE.stage3;
		} else {
			return YEAR_RELEASE.stage4;
		}
	}

	function getColor() {
		let number = Math.random() * 100;
		
		if (number <= 40) {
			return COLOR.black;
		} else if (number <= 82) {
			return COLOR.white;
		} else if (number <= 88) {
			return COLOR.red;
		} else if (number <= 94) {
			return COLOR.yellow;
		} else {
			return COLOR.green;
		}
	}

	function getOwner() {
		let number = Math.random() * 100;
		
		if (number <= 20) {
			return OWNER.stage1;
		} else if (number <= 40) {
			return OWNER.stage2;
		} else if (number <= 70) {
			return OWNER.stage3;
		} else {
			return OWNER.stage4;
		}
	}


	function getCountOfPassanger() {
		let number = Math.random() * 100;
		
		if (number <= 30) {
			return COUNT_OF_PASSANGER.stage1;
		} else if (number <= 50) {
			return COUNT_OF_PASSANGER.stage2;
		} else if (number <= 60) {
			return COUNT_OF_PASSANGER.stage3;
		} else if (number <= 70) {
			return COUNT_OF_PASSANGER.stage4;
		} else if (number <= 85) {
			return COUNT_OF_PASSANGER.stage5;
		} else {
			return COUNT_OF_PASSANGER.stage6;
		}
	}