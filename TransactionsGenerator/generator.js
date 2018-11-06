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
		
		if (number <= 35) {
			return SEASON_CONST.winter;
		} else if (number <= 55) {
			return SEASON_CONST.summer;
		} else if (number <= 80) {
			return SEASON_CONST.autumn;
		} else {
			return SEASON_CONST.spring
		}
	}
