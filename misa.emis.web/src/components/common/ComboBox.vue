<template
  ><div>
    <div class="combobox"  v-on:keyup.13="onClick()">
      <div
        class="icon"
        id="dropdown-icon"
        :style="{ transform: 'rotate(' + degree + ')' }"
        v-on:click.stop="onClick()"
       
      ></div>
      <InputVue
        :type="type"
        :label="label"
        :value.sync="value1"
        :errorMess="errorMess"
        :placeholder="listData[0].text"
        :onChange="onChange"
        :required="required"
        readonly
      />

      <div class="select" :class="{ show: show }" name="department">
        <div
          class="option"
          :class="{ active: active === data.id }"
          v-for="(data, index) in listData"
          :key="index"
          :value="data.id"
          :tabindex="1"
          @click="onSelect(data)"
          v-on:keyup.13="onEnter(data)"
        >
          {{ data.text }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import InputVue from "../common/InputVue";
export default {
  name: "comboBox",
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

    listData: {
      type: Array,
      default: null,
    },
    active: {
      type: String,
      default: "",
    },
    style: {
      type: Object,
      default: null,

    },
  },
  data() {
    return {
      degree: "90deg",
      value1: "",
    };
  },
  computed: {
    show() {
      if (this.degree === "90deg") return false;
      else return true;
    },
  },
  methods: {
    onClick() {
      this.degree = this.degree === "90deg" ? "270deg" : "90deg";
    },
    onSelect(data) {
      this.value1 = data.text;
      this.$emit("update:value", data.id);
   this.degree = "90deg";
    },
    onChange(value) {
      this.value1 = value;
      if (value === "") this.$emit("update:value", "0");
      this.degree = "90deg";
    },
    onEnter(data) {
        this.onSelect(data);
         this.degree = "270deg";
    }
  },
  components: {
    InputVue,
  },
  async created(){
  this.listData.forEach((item)=>{if(item.id===this.value) this.value1=item.text;})
  }
};
</script>

<style>

</style>
