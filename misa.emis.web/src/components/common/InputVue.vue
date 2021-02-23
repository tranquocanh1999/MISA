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
      v-bind:type="type"
      class="input_content "
      v-bind:class="{ errorValidate: error, prefix: hasPrefix }"
      id="input_content "
      v-bind:value="value"
      v-bind:style="{ backgroundImage: 'url(' + prefix + ')', styles }"
      v-on:keyup="onChange($event.target.value)"
      v-bind:placeholder="placeholder"
      :readonly="readonly"
      :required="required"
      v-on:focus="autofocus"
      tabindex="1"
      ref="autoFocus"
    />

    <div
      class=" input_icon "
      v-on:click="onChange(type === 'number' ? 0 : '')"
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
  },
  methods: {},
created(){

  if(this.autofocus===true) setTimeout(()=>{this.$refs.autoFocus.focus()},0);
}

};
</script>

<style></style>
