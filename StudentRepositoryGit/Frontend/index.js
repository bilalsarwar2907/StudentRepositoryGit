// use https if possible in real apps 
const baseUrl = "https://localhost:7137/api/Students";

Vue.createApp({

  data() {
    return {
      students: [],
      allStudents: [],
        studentsMessage: "",
        singleStudent: null,
        deleteId: null,
        deleteMessage: "",
        addData: {name:"", birthYear: null, grade: null},
        addMessage: "",
        updateData: {id: null, name:"", birthYear: null, grade: null},
        updateMessage: "",
      }
    },

    async created() {
      await this.getAllStudents();
    },

    methods: {
        //------------------- GET ALL STUDENTS ------------------
        getAllStudents() {
            this.getStudents(baseUrl);
        },

        async getStudents(url) {
            try {
                const response = await axios.get(url);
               this.students = response.data;
               this.allStudents = response.data;
            }
            catch (ex) {
                alert(ex.message);
            }
        },

        // ================= SORTING =================
       sortById() {
            this.students.sort((s1, s2) => s1.id - s2.id);
        },
        sortByName() {
            this.students.sort((s1, s2) => s1.name.localeCompare(s2.name));
        },
        sortByAge() {
            this.students.sort((s1, s2) => s1.birthYear - s2.birthYear);
        },
        sortByAgeDescending() {
            this.students.sort((s1, s2) => s2.birthYear - s1.birthYear);
        },
        sortByAgeAscending() {
            this.students.sort((s1, s2) => s1.birthYear - s2.birthYear);
        },

       sortByGradeDescending() {
       this.students.sort((s1, s2) => s2.grade.localeCompare(s1.grade));
},
       sortByGradeAscending() {
        this.students.sort((s1, s2) => s1.grade.localeCompare(s2.grade));
},
 // ================= FILTER =================
        filterByName(name) {
            this.students = this.allStudents.filter(s =>
                s.name.toLowerCase().includes(name.toLowerCase()))
        },
        filterByGrade(grade) {
            this.students = this.allStudents.filter(s => s.grade === grade);
        },
        // ================= GET BY ID =================
        async getById(id) {
            if (id === null || id === undefined || isNaN(id) || id <= 0) {
                alert("Please enter a valid student ID")
                return
            }

            const url = baseUrl + "/" + id

            try {
                const response = await axios.get(url)
                this.singleStudent = await response.data
            } catch (ex) {
                this.single = null
                alert(ex.message)
            }
        },
         // ================= DELETE =================
        async deleteStudent(deleteId) {
            if (deleteId === null || deleteId === undefined || isNaN(deleteId) || deleteId <= 0) {
                alert("Please enter a valid student ID")
                return
            }

            const url = baseUrl + "/" + deleteId

            try {
                const response = await axios.delete(url)
                this.deleteMessage = response.status + " " + response.statusText
                this.getAllStudents()
            } catch (ex) {
                alert(ex.message)
            }
        },

        // ================= ADD =================
        async addStudent() {
            if (this.addData.name === "" || this.addData.birthYear === null || this.addData.birthYear <= 0 || this.addData.grade === null || this.addData.grade <= 0) {
                alert("Please fill in all fields with valid values")
                return
            }

            try {
                const response = await axios.post(baseUrl, this.addData)
                this.addMessage = "response " + response.status + " " + response.statusText
                this.getAllStudents()
            } catch (ex) {
                alert(ex.message)
            }
        },

         // ================= UPDATE =================
        async updateStudent() {
            if (this.updateData.id === null || this.updateData.id === undefined || isNaN(this.updateData.id) || this.updateData.id <= 0) {
                alert("Please enter a valid student ID")
                return
            }

            const url = baseUrl + "/" + this.updateData.id

            try {
                const response = await axios.put(url, this.updateData)
                this.updateMessage = "response " + response.status + " " + response.statusText
                this.getAllStudents()
            } catch (ex) {
                alert(ex.message)
            }
        }

    }

}).mount("#app")
