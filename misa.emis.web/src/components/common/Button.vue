<template>
  <button
    class="btn"
    :class="{noContent:content===''}"
    v-on:click="onClick"
    v-bind:style="{ width: width, styles }"
    :disabled="disabled"
  >
    <div
      class="icon"
      v-bind:style="{ backgroundImage: 'url(' + icon + ')' }"
      v-if="hasIcon"
    ></div>
    {{ content }}
  </button>
</template>

<script>
export default {
  name: "Button",
  props: {
    content: {
      type: String,
      default: "",
    },
  
    styles: {
      type: String,
      default: "",
    },
    icon: {
      type: String,
      default: "",
    },
    onClick: {
      type: Function,
      default: () => {},
    },
     disabled: {
      type: Boolean,
      default:false,
    },
  },
  computed: {
    width() {
      if (this.content.split(" ").length < 3) return 100;
      else return 48 + this.size * this.content.length;
    },
    hasIcon() {
      if (this.icon === "" || this.icon === null) return false;
      else return true;
    },
  },
  data() {
    return {};
  },
};
</script>

<style>
button {
  display: flex;
  align-items: center;
  border-radius: 4px;
  height: 32px;
  font-size: 13px;
  color: #ffffff;
  padding: 0 16px;
  text-align: center;
  margin:0px 4px;
}

.noContent {
  padding: 0 0 0 8px!important;
}
.btn {
  background-color: #03AE66;
}

.btn:hover {
  background-color: #02BF70;
  cursor: pointer;
}

.btn:active {
  background-color: #01b075;
}



.btn-default {
  background-color: #FFFFFF;
  color: #3D3F4E;
  border: 1px solid #3D3F4E;
}

.btn-default:hover {
  background-color: #F8F8F8;
}


.btn .icon {
  width: 20px;
  height: 20px;
  background-repeat: no-repeat;
  background-size: contain;
  margin-right: 8px;
}
</style>
