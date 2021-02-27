<template>
  <div class="input" v-bind:id="id">
    <label for="input_content" class="input_label">
      {{ label }}
      <span style="color:red" v-if="required === true">(*)</span></label
    >
    <div class="popup">
      <span class="popuptext " v-bind:class="{ show: error }" id="errormes">
        {{ errorMess }}</span
      >
    </div>
    <input
      type="text"
      class="input_content "
      v-bind:class="{ errorValidate: error, prefix: hasPrefix }"
      id="input_content"
      v-bind:value="checkValue"
      v-bind:style="{ backgroundImage: 'url(' + prefix + ')', styles }"
      v-on:keyup="onKeyup"
      v-on:keydown="onKeydown"
      v-bind:placeholder="placeholder"
      :readonly="readonly"
      :required="required"
      v-on:focus="autofocus"
      tabindex="1"
      ref="autoFocus"
    />

    <div
      class=" input_icon "
      v-on:click="onChange(type === 'currency' ? 0 : '')"
      v-if="hasValue"
    ></div>
  </div>
</template>

<script>
export default {
  name: "InputVue",
  props: {
    id: {
      type: String,
      default: "",
    },
    label: {
      type: String,
      default: "",
    },
    type: {
      type: String,
      default: "text",
    },
    value: {
      type: String,
      default: "",
    },
    errorMess: {
      type: String,
      default: "",
    },
    required: {
      type: Boolean,
      default: false,
    },
    autofocus: {
      type: Boolean,
      default: false,
    },
    placeholder: {
      type: String,
      default: "",
    },

    prefix: {
      type: String,
      default: "",
    },
    readonly: {
      type: Boolean,
      default: false,
    },
    onChange: {
      type: Function,
      default: () => 1,
    },
    style: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {};
  },
  computed: {
    error() {
      if (this.errorMess === "" || this.errorMess === null) return false;
      else return true;
    },
    hasValue() {
      if (this.value === "" || this.value === null) return false;
      else return true;
    },
    hasPrefix() {
      if (this.prefix === "" || this.prefix === null) return false;
      else return true;
    },
    checkValue() {
      if (this.type === "currency") {
        return this.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
      } else return this.value;
    },
  },
  methods: {
   async onKeydown(e) {
    
      if (
        !(
          (e.keyCode > 95 && e.keyCode < 106) ||
          (e.keyCode > 47 && e.keyCode < 58) ||
          e.keyCode == 8|| e.keyCode == 9
        )&&this.type === "currency"
      ) {
       e.preventDefault()
      }
    },
    async onKeyup(e) {
          var x = e.target.value;
      if (this.type === "currency") {
       
     if(x===null||x==='') x="0"
        x = parseInt(x.split(",").join(""));
   
      }
           this.onChange(x);
    },
  },
  created() {
    if (this.autofocus === true)
      setTimeout(() => {
        this.$refs.autoFocus.focus();
      }, 0);
  },
};
</script>

<style></style>
