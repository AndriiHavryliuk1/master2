const fs = require('fs');
const path = require('path');

const STATE_CONST =  {
	man: "a",
	woman: "b"
}

const AGE_CONST = {
	stage1: "c",
	stage2: "d",
	stage3: "e",
	stage4: "f",
	stage5: "g",
	stage6: "h",
	stage7: "i",
	stage8: "j"
}

const ALCOHOL_CONST = {
	stage1: "k",
	stage2: "l",
	stage3: "m",
	stage4: "n",
	stage5: "o"
}

const TIME_CONST = {
	stage1: "p",
	stage2: "q",
	stage3: "r",
	stage4: "s"
}
const SEASON_CONST = {
	summer: "w",
	autumn: "x",
	winter: "y",
	spring: "x",
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
			result += `${getState()}`;
			result += `${getAge()}`;
			result += `${getAlco()}`;
			result += `${getTime()}`;
			result += `${getSeason()},`;
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
