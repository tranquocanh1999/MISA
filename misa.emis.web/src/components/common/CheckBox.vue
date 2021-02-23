<template>
  <div class="checkBox" :style="styles">
    <label class="checkBox__container" :class="{ disabled: disabled }">
      {{ content }}
      <input
        type="checkbox"
        v-on:click="onClick(value)"
        :checked="checked === true"
        :disabled="disabled"
        tabindex="-1"
      />
      <span
        class="checkBox__checkmark"
        tabindex="0"
        v-on:keyup.13="onKeyup()"
      ></span>
    </label>
  </div>
</template>

<script>
export default {
  name: "CheckBox",
  props: {
    content: {
      type: String,
      default: "",
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    styles: {
      type: String,
      default: "",
    },
    value: {
      type: String,
      default: "",
    },
    checked: {
      type: Boolean,
      default: false,
    },
    onClick: {
      type: Function,
      default: () => 1,
    },
  },
  data() {
    return {};
  },
  methods: {
    onKeyup: function() {
      
      this.onClick(this.value);
      if(!this.disabled)
      this.checked=!this.checked;
     
    },
  },
};
</script>

<style>
</style>
