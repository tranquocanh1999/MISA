<template>
  <div name="dialog" class="fee__detail">
    <div v-if="active" class="dialog-backdrop" @click="handleBackdropClick">
      <div class="dialog-container" @click.stop ref="dialog">
        <div
          class="icon__cancel"
          tabindex="1"
          v-on:click="handleBackdropClick"
          v-on:keyup.13="handleBackdropClick"
        ></div>
        <h1 class="dialog__title">{{ title }}</h1>
        <div class="input__form">
          <div class="left">
            <InputVue
              type="text"
              label="Tên khoản thu"
              :value="fee2.feeName"
              :onChange="
                (e) => {
                  fee2.feeName = e;
                }
              "
              :errorMess="errorMess.feeNameErrMess"
              autofocus
              required
            />
            <div class="feeGroup">
              <ComboBox
                label="Nhóm khoản thu"
                style="width:calc(100% - 40px)"
                :value.sync="fee2.feeGroupID"
                :onChange="
                  (e) => {
                    fee2.feeGroupID = e;
                  }
                "
                :active="fee2.feeGroupID"
                :listData="feeGroup"
              ></ComboBox>
              <div>
                <button tabindex="1" class="btn-default"></button>
              </div>
            </div>
            <div class="Price">
            
              <InputVue
                type="currency"
                label="Mức thu"
                required
                style="width:70%"
                :value="fee2.price"
                :onChange="
                  (e) => {
                    fee2.price = e;
                  }
                "
                :errorMess="errorMess.priceErrMess"
              />
              <div class="text">đ/</div>
              <ComboBox
                label="Đơn vị tính"
                required
                style="width:25%"
                :listData="units"
                :value.sync="fee2.unit"
                :active="fee2.unit"
                :onChange="
                  (e) => {
                    fee2.unit = e;
                  }
                "
                :errorMess="errorMess.unitErrMess"
              ></ComboBox>
            </div>
            <div class="applyObject">
              <ComboBox
                label="Phạm vi thu"
                required
                :listData="applyObject"
                :value.sync="fee2.applyObject"
                :active="fee2.applyObject"
                :onChange="
                  (e) => {
                    fee2.applyObject = e;
                  }
                "
                :errorMess="errorMess.applyObjectErrMess"
              ></ComboBox>
            </div>
            <div class="property">
              <ComboBox
                label="Tính chất"
                :listData="property"
                :value.sync="fee2.property"
                :active="fee2.property"
                :onChange="
                  (e) => {
                    fee2.property = e;
                  }
                "
                :errorMess="errorMess.periodErrMess"
              ></ComboBox>
            </div>

            <div class="period ">
              <div class="label">
                Kỳ thu
                <div style="color:red">(*)</div>
              </div>
              <div></div>
              <div class="period__input">
                <div
                  class="period-item"
                  v-for="(item, index) in period"
                  :key="index"
                  tabindex="1"
                  @keyup.13="fee2.period = item.id"
                >
                  <input
                    type="radio"
                    :id="item.id"
                    :value="item.id"
                    :checked="fee2.period === item.id"
                    v-model="fee2.period"
                    tabindex="-1"
                  />
                  <label :for="item.id">{{ item.text }}</label>
                </div>
              </div>
            </div>
          </div>
          <div class="right">
            <div style="display:flex">
              <CheckBox
                content="Áp dụng miễn giảm"
                styles="width:250px;"
                :checked="fee2.isApplyRemisson"
                :value="fee2.isApplyRemisson"
                :onClick="
                  (e) => {
                    fee2.isApplyRemisson = !e;
                  }
                "
              />
              <CheckBox
                content="Cho phép xuất chứng từ"
                styles="width:250px;"
                :checked="fee2.allowCreateReceipt"
                :value="fee2.allowCreateReceipt"
                :onClick="
                  (e) => {
                    fee2.allowCreateReceipt = !e;
                  }
                "
              />
            </div>
            <div style="display:flex">
              <CheckBox
                content="Khoản thu bắt buộc"
                styles="width:250px;"
                :checked="fee2.isRequire"
                :value="fee2.isRequire"
                :onClick="
                  (e) => {
                    fee2.isRequire = !e;
                  }
                "
              />
              <CheckBox
                content="Cho phép hoàn trả"
                styles="width:250px;"
                disabled
              />
            </div>

            <div style="display:flex">
              <CheckBox
                content="Cho phép xuất hóa đơn"
                styles="width:250px;"
                :checked="fee2.allowCreateInvoice"
                :value="fee2.allowCreateInvoice"
                :onClick="
                  (e) => {
                    fee2.allowCreateInvoice = !e;
                  }
                "
              />
              <CheckBox
                content="Thu nội bộ"
                styles="width:250px;"
                :checked="fee2.isInternal"
                :value="fee2.isInternal"
                :onClick="
                  (e) => {
                    fee2.isInternal = !e;
                  }
                "
              />
            </div>
          </div>
        </div>

        <div class="dialog__footer">
          <CheckBox
            content="ngừng theo dõi"
            styles="width:250px;"
            v-if="this.title!=='Thêm mới khoản thu'"
            :checked="!fee2.isActive"
            :value="!fee2.isActive"
            :onClick="
              (e) => {
                fee2.isActive = e;
              }
            "
          />

          <div class="button">
            <button
              class="btn-default"
              style="width:92px;padding-left:24px"
              @click="handleBackdropClick"
            >
              <h3>Đóng</h3>
            </button>
            <button
              class="btn"
              style="width:92px;padding-left:24px"
              @click="onSubmit(fee2)"
            >
              <h3>Lưu</h3>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import CheckBox from "../../../components/common/CheckBox";
import InputVue from "../../../components/common/InputVue";
import ComboBox from "../../../components/common/ComboBox";
import unit from "../../../assets/unit.JSON";
import applyObject from "../../../assets/applyObject.JSON";
import property from "../../../assets/property.JSON";
import period from "../../../assets/period.JSON";
export default {
  name: "FeeDetail",
  props: {
    active: {
      type: Boolean,
      default: false,
    },
    errorMess: {
      type: Object,
      default: null,
    },
    title: {
      type: String,
      default: "",
    },
    fee: {
      type: Object,
      default: null,
    },
    onSubmit: {
      type: Function,
      default: () => 1,
    },
    feeGroup: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      units: unit,
      applyObject: applyObject,
      property: property,
      period: period.slice(1,5),
      a: 1,
      fee2: this.fee,
    };
  },

  methods: {
    handleBackdropClick() {
      this.$emit("update:active", false);
    },
  },
  components: {
    InputVue,
    ComboBox,
    CheckBox,
  },
  async created() {
   
  },
};
</script>

<style scoped></style>
