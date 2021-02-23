<template>
  <v-app :class="{ showDialog: isShowDialog }">
    <Navbar :active="navbarActive" :onClick="onChangeNavbar" />
    <Header />
    <Content
      :active="tabActive"
      :onClick="onChangeTab"
    
    />
  </v-app>
</template>

<script>
import Content from "./components/layout/Content";
import Header from "./components/layout/Header";
import Navbar from "./components/layout/Navbar";

export default {
  name: "App",

  components: {
    Content,
    Header,
    Navbar,
  },

  data: () => ({
    navbarActive: 1,
    tabActive: 1,
 
  }),
  methods: {
    onChangeNavbar(index) {
      this.navbarActive = index;
    },
    onChangeTab(index) {
      this.tabActive = index;
    }
  },
};
</script>
<style scope>
@import "./styles/index.css";


</style>
